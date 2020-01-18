using System;
using System.Collections.Generic;
using NKnife.MeterKnife.Util.Electronics.Collections;
using NKnife.MeterKnife.Util.Electronics.Exceptions;

namespace NKnife.MeterKnife.Util.Electronics.Helper
{
    /// <summary>一个面向电阻值分解的助手类
    /// </summary>
    public class ResistanceValueResolve
    {
        /// <summary>已存在的电阻的集合
        /// </summary>
        public Resistances Existing { get; set; }

        /// <summary>将指定的电阻值从已存在的电阻集合中搜索最合适的电阻进行组合，并返回最合适的电阻的集合
        /// </summary>
        /// <param name="results">返回的结果（最合适的分解后的电阻值群）集合，</param>
        /// <param name="model">分解阻值的模式</param>
        /// <param name="target">指定的需分解的电阻值,单位欧姆(Ω)</param>
        /// <param name="begin">已存在的集合的起始位置，默认从0的位置开始搜索</param>
        /// <returns>分解剩余的阻值余数</returns>
        public double TryGetResolveResult(out Resistances[] results, ResistanceValueResolveModel model, double target, int begin = 0)
        {
            if (Existing == null || Existing.Count <= 0)
                throw ResistanceCollectionException.ForNull();
            if (target < 0)
                throw new ArgumentException("需分解的电阻值不能小于零.");

            var resultlist = new List<Resistances>();

            //分解剩余的阻值余数
            double remainder = target;

            if ((model & ResistanceValueResolveModel.All) == ResistanceValueResolveModel.SeriesBigValuePriority)
            {
                var result = new Resistances();
                remainder = TryGetSeriesBigValuePriorityResult(ref result, target, begin);
                if (result != null && result.Count >= 0)
                    resultlist.Add(result);
            }
            if ((model & ResistanceValueResolveModel.All) == ResistanceValueResolveModel.SeriesSmallValuePriority)
            {
                var result = new Resistances();
                remainder = TryGetSeriesSmallValuePriorityResult(ref result, target, Existing.Count);
                if (result != null && result.Count >= 0)
                    resultlist.Add(result);
            }
            if ((model & ResistanceValueResolveModel.All) == ResistanceValueResolveModel.ParallelingSimple)
            {
                var result = new Resistances();
                remainder = TryGetSeriesBigValuePriorityResult(ref result, target, begin);
                if (result != null && result.Count >= 0)
                    resultlist.Add(result);
            }

            results = resultlist.ToArray();
            return remainder;
        }

        /// <summary>按串联电路,大阻值优先进行顺序分解
        /// </summary>
        /// <param name="result">返回的结果，最合适的电阻的集合</param>
        /// <param name="target">指定的需分解的电阻值,单位欧姆(Ω)</param>
        /// <param name="begin">已存在的集合的起始位置，默认从0的位置开始搜索</param>
        /// <returns>分解剩余的阻值余数</returns>
        protected double TryGetSeriesBigValuePriorityResult(ref Resistances result, double target, int begin)
        {
            //下面代码中我们称已存在的电阻的集合为“数列”，并假定该数列已按大到小进行过排序
            for (int i = begin; i < Existing.Count; i++)
            {
                if (target <= 0) //当递归回归主函数时
                    break;
                if (target < Existing[i].Value) //如果目标值小于集合当前值，继续向数列下一个进行寻找合适值
                    continue;
                Resistance res = Existing[i];
                if (target/res.Value > 1)
                {
                    var count = (int) (target/res.Value);
                    for (int j = 0; j < count; j++)
                    {
                        result.Add((Resistance) res.Clone());
                        target -= res.Value;
                    }
                }
                //余数进行下一步的递归分解
                double modular = target%res.Value;
                if (modular > 0)
                {
                    //递归分解
                    target = TryGetSeriesBigValuePriorityResult(ref result, modular, i);
                }
                else
                {
                    break;
                }
            }
            return target;
        }

        private Resistances _SmallPrioritResult;

        /// <summary>按串联电路，以小阻值优先进行分解，以阻值品种最少为最终结果。
        /// 反向搜索，结果会较多，进行一一比较，时间复杂度（Oend）
        /// </summary>
        /// <param name="result">返回的结果,最合适的电阻的集合</param>
        /// <param name="target">指定的需分解的电阻值,单位欧姆(Ω)</param>
        /// <param name="end">本函数将从集合尾部进行搜索组合</param>
        /// <returns>分解剩余的阻值余数</returns>
        protected double TryGetSeriesSmallValuePriorityResult(ref Resistances result, double target, int end)
        {
            if (end < 0 || end >= Existing.Count)
                end = Existing.Count - 1;

            if (result.Count == 0 && (_SmallPrioritResult == null || _SmallPrioritResult.Count > 0))
                _SmallPrioritResult = new Resistances();

            //下面代码中我们称已存在的电阻的集合为“数列”，并假定该数列已按大到小进行过排序
            for (int i = end; i >= 0; i--)//反向循环
            {
                var resValue = Existing[i].Value;
                if (resValue == 1) 
                    continue;
                var count = (int) (target/resValue);
                // 当目标值可分解，且分解的份数小于上次分解的份数
                if (count > 1 && count > _SmallPrioritResult.Count)
                {
                    result.Clear();
                    for (int j = 0; j < count; j++)
                        result.Add((Resistance) Existing[i].Clone());
                }
                var remainder = target%resValue;
                if (remainder>0)
                {
                    var bigResult = new Resistances();
                    remainder = TryGetSeriesBigValuePriorityResult(ref bigResult, remainder, i);
                    if (bigResult.Count > 0)
                    {
                        foreach (var res in bigResult)
                            _SmallPrioritResult.Add(res);
                    }
                    foreach (var res in _SmallPrioritResult)
                    {
                        result.Add(res);
                    }
                    _SmallPrioritResult.Clear();
                    //return remainder;
                }
            }
            return target;
        }
    }
}