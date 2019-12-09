using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Array1 : MonoBehaviour
{
    public Num[] arrayNum = null;
    private int[] _arrayNumIndex;
    private int[] _arrayTemp;
    SortedDictionary<int,List<int>> _dicBucket = new SortedDictionary<int, List<int>>();
    
    // Start is called before the first frame update
    void Start()
    {
        _arrayNumIndex = new int[arrayNum.Length];
        for(int i =0; i < arrayNum.Length; i++)
        {
            _arrayNumIndex[i] = arrayNum[i].mNumIndex;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void ResetArrayNum()
    {
        for(int i = 0; i < _arrayNumIndex.Length;i++)
        {
            arrayNum[i].mNumIndex = _arrayNumIndex[i];
            arrayNum[i].UpdateText();
        }
        _dicBucket.Clear();
        _arrayCount = null;
    }
    
    #region 计数排序
    
    private int[] _arrayCount;
    
    
    public void StartCountingSort()
    {
        CountingSort();
    }

    void CountingSort()
    {
        int min = arrayNum[0].mNumIndex, max = arrayNum[0].mNumIndex;
        foreach (var t in arrayNum)
        {
            if (min > t.mNumIndex)
            {
                min = t.mNumIndex;
            }

            if (max < t.mNumIndex)
            {
                max = t.mNumIndex;
            }
        }

        int arrayCountNum = GetCountIndex(max,min) + 1;
        _arrayCount = new int[arrayCountNum];

        foreach (var t in arrayNum)
        {
            int index = GetCountIndex(t.mNumIndex, min);
            _arrayCount[index]++;
        }

        for (int i = 1; i < _arrayCount.Length; i++)
        {
            _arrayCount[i] = _arrayCount[i - 1] + _arrayCount[i];
        }

        int[] temp = new int[arrayNum.Length];
        for(int i = 0; i < arrayNum.Length; i++) 
        {
            int index = GetCountIndex(arrayNum[i].mNumIndex, min);
            temp[_arrayCount[index] - 1] = arrayNum[i].mNumIndex;
            _arrayCount[index]--;
        }

        for (int i = 0; i < temp.Length; i++)
        {
            arrayNum[i].mNumIndex = temp[i];
            arrayNum[i].UpdateText();
        }
    }
    
    //获取转换后count数组的index
    int GetCountIndex(int x, int min)
    {
        return x - min;
    }

    #endregion

    #region 桶排序
    
    public void StartBucketSort()
    {
        BucketSort();
    }

    void BucketSort()
    {
        int index;
        foreach (var t in arrayNum)
        {
            index = GetBucketIndex(t.mNumIndex);
            if (!_dicBucket.ContainsKey(index))
            {
                _dicBucket.Add(index,new List<int>());
                _dicBucket[index].Add(t.mNumIndex); 
            }
            else
            {
                int i = 0;
                bool isInsert = false;
                foreach (var num in _dicBucket[index])
                {
                    if (num > t.mNumIndex)
                    {
                        isInsert = true;
                        _dicBucket[index].Insert(i, t.mNumIndex);
                        break;
                    }
                    i++;
                }

                if (!isInsert)
                {
                    _dicBucket[index].Add(t.mNumIndex);
                }
            }
        }

        index = 0;
        foreach(var t in _dicBucket.Keys)
        {
            foreach(var v in _dicBucket[t])
            {
                arrayNum[index].mNumIndex = v;
                arrayNum[index].UpdateText();
                index++;
            }
        }
    }

    int GetBucketIndex(int num)
    {
        return num / 10 == 0 ? 1 : num / 10;
    }
    
    #endregion

    #region 基数排序

    public void StartRadixSort()
    {
        _dicBucket.Clear();
        _arrayTemp = new int[arrayNum.Length];
        RadixSort();
    }

    void RadixSort()
    {
        int index = GetRadixIndex();
        
        for(int i = 0; i < arrayNum.Length;i++)
        {
            _arrayTemp[i] = arrayNum[i].mNumIndex;
        }
        
        for (int i = 1; i <= index; i++)
        {
            _dicBucket.Clear();
            RadixSortByIndex(i);
        }

        for (int i = 0; i < _arrayTemp.Length; i++)
        {
            arrayNum[i].mNumIndex = _arrayTemp[i];
            arrayNum[i].UpdateText();
        }
        
    }

    void RadixSortByIndex(int index)
    {
        for (int i = 0; i < _arrayTemp.Length; i++)
        {
            int radix = GetRadix(_arrayTemp[i], index);
            if (!_dicBucket.ContainsKey(radix))
            {
                _dicBucket[radix] = new List<int>();
            }
            
            _dicBucket[radix].Add(_arrayTemp[i]);
        }

        int k = 0;
        foreach (var list in _dicBucket)
        {
            foreach (var t in list.Value)
            {
                _arrayTemp[k++] = t;
            }
        }
    }
    
    int GetRadixIndex()
    {
        int max = arrayNum[0].mNumIndex;
        foreach (var t in arrayNum)
        {
            if (t.mNumIndex > max)
            {
                max = t.mNumIndex;
            }
        }

        int index = 0;
        while (max > 0)
        {
            max -= (int)Math.Pow(10,index) * 9;
            index++;
        }
        
        return index == 0?1:index;
    }

    int GetRadix(int num,int radix)
    {
        if (radix <= 0)
        {
            return 0;
        }

        int index = num % (int)Math.Pow(10,radix);
        return index / (int) Math.Pow(10, radix-1);
    }
    
    #endregion
}
