using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

[DisallowMultipleComponent]
public class Main : MonoBehaviour
{
  [SerializeField, Tooltip("到達対象")]
  private Transform TARGET_POINT = null;

  // 定数
  [SerializeField, Tooltip("最大到達世代数"), Header("環境設定")]
  private int MAX_AGE = 100;
  [SerializeField, Tooltip("ゲノム数")]
  private int MAX_GENOM_COUNT = 100;
  [SerializeField, Tooltip("遺伝子長")]
  private int GENOM_LENGTH = 10;

  [SerializeField, Tooltip("選択ルール"), Header("世代更新設定")]
  private SelectRuleMode s_rule = SelectRuleMode.Elite;
  [SerializeField, Tooltip("交叉ルール")]
  private CrossOverRuleMode c_rule = CrossOverRuleMode.Single;
  [SerializeField, Tooltip("突然変異発生確率(%)"), Range(0, 100)]
  private float m_rate = 1.0f;

  private int age = 1;
  private GameObject[] cubes;

  private ISelectRule<Vector2> selectRule;
  private ICrossOverRule<Vector2> crossOverRule;
  private IMutationRule<Vector2> mutationRule;

  private enum SelectRuleMode { Elite, Roulette }
  private enum CrossOverRuleMode { Single, Two }

  IEnumerator Start()
  {
    // ルールの設定
    SetupRules();

    // ゲノム生成
    var genoms = new VectorGenom[MAX_GENOM_COUNT];
    for (int i = 0; i < genoms.Length; i++)
    {
      genoms[i] = new VectorGenom(GENOM_LENGTH);
    }

    // ゲノムを適用するキューブを用意
    cubes = new GameObject[MAX_GENOM_COUNT];
    for (int i = 0; i < cubes.Length; i++)
    {
      cubes[i] = GameObject.CreatePrimitive(PrimitiveType.Cube);
    }

    var pase = new WaitForSeconds(0.2f);

    while(true)
    {
      for(int i = 0; i < cubes.Length; i++)
      {
        var v = genoms[i].OutputDna();
        cubes[i].transform.position = new Vector3(v.x, 0, v.y);
      }

      // 終了
      if (age >= MAX_AGE) break;

      // ゲノムのスコアリング
      for(int i = 0; i < genoms.Length; i++)
      {
        var vg = genoms[i].OutputDna();
        genoms[i].Score = Mathf.Sqrt(Mathf.Pow(TARGET_POINT.position.x - vg.x, 2) + Mathf.Pow(TARGET_POINT.position.z - vg.y, 2));
      }

      // 優秀順に並べ替え
      Array.Sort(genoms, (a, b) => a.Score > b.Score ? 1 : a.Score == b.Score ? 0 : -1);

      // 選択
      var selected = selectRule.Select(genoms, MAX_GENOM_COUNT / 5);

      // 交差
      List<VectorGenom> nGenoms = new List<VectorGenom>();
      for (int i = 0; i < selected.Length; i++)
      {
        nGenoms.Add((VectorGenom)selected[i]);
      }
      while(nGenoms.Count < MAX_GENOM_COUNT)
      {
        var ch = new VectorGenom(GENOM_LENGTH);
        crossOverRule.CrossOverDna(
          genoms[UnityEngine.Random.Range(0, genoms.Length)],
          genoms[UnityEngine.Random.Range(0, genoms.Length)],
          ch
        );
        nGenoms.Add(ch);
      }
      genoms = nGenoms.ToArray();

      // 突然変異
      for(int i = 0; i < genoms.Length; i++)
      {
        if (UnityEngine.Random.value < m_rate)
        {
          mutationRule.Mutate(genoms[i]);
        }
      }

      age++;

      yield return pase;
    }
  }

  void SetupRules()
  {
    switch(s_rule)
    {
      case SelectRuleMode.Elite: selectRule = new EliteSelectRule<Vector2>(); break;
      case SelectRuleMode.Roulette: selectRule = new RouletteSelectRule<Vector2>(); break;
      default: selectRule = new EliteSelectRule<Vector2>(); break;
    }

    switch(c_rule)
    {
      case CrossOverRuleMode.Single: crossOverRule = new SinglePointCrossOverRule<Vector2>(); break;
      case CrossOverRuleMode.Two: crossOverRule = new TwoPointCrossOverRule<Vector2>(); break;
      default: crossOverRule = new SinglePointCrossOverRule<Vector2>(); break;
    }

    mutationRule = new VectorMutationRuls();
    m_rate /= 100;
  }
}
