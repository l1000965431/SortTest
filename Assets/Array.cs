using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Array : MonoBehaviour
{
    public Num[] arrayNum = new Num[10];
    private int[] _arrayNumIndex;

    // Start is called before the first frame update
    void Start()
    {
        if(arrayNum == null)
        {
            Debug.LogError("arrayNum == null");
            return;
        }

        if(arrayNum.Length < 10)
        {
            Debug.LogError("arrayNum:" + arrayNum.Length);
            return;
        }

        _arrayNumIndex = new int[arrayNum.Length];
        for(int i =0; i < arrayNum.Length; i++)
        {
            _arrayNumIndex[i] = arrayNum[i].mNumIndex;
        }
    }

    public void ResetArrayNum()
    {
        for(int i = 0; i < _arrayNumIndex.Length;i++)
        {
            arrayNum[i].mNumIndex = _arrayNumIndex[i];
            arrayNum[i].UpdateText();
        }
    }

    void ChangeNumPos(int index1,int index2)
    {
        int temp = arrayNum[index1].mNumIndex;
        arrayNum[index1].mNumIndex = arrayNum[index2].mNumIndex;
        arrayNum[index1].UpdateText();
        arrayNum[index2].mNumIndex = temp;
        arrayNum[index2].UpdateText();
    }

    #region 冒泡排序  O(n^2)
    public void StartBubbleSort()
    {
        StartCoroutine(BubbleSort());
    }


    IEnumerator BubbleSort()
    {
        for(int i = 0; i < arrayNum.Length-1;i++)
        {
            for(int j = 0; j < arrayNum.Length - 1 - i; j++)
            {
                if(arrayNum[j].mNumIndex > arrayNum[j+1].mNumIndex)
                {
                    ChangeNumPos(j, j + 1);
                }
            }
            yield return new WaitForSeconds(1.0f);
        }
    }
    #endregion


    #region 选择排序 O(n^2)
    public void StartSelectionSort()
    {
        StartCoroutine(SelectionSort());
    }

    IEnumerator SelectionSort()
    {
        for (int i = 0; i < arrayNum.Length - 1; i++)
        {
            int min = i;
            for(int j = i+1; j < arrayNum.Length; j++)
            {
                if(arrayNum[j].mNumIndex < arrayNum[min].mNumIndex)
                {
                    min = j;
                }
            }
            ChangeNumPos(i, min);
            yield return new WaitForSeconds(1.0f);
        }
    }
    #endregion


    #region 插入排序 O(n^2)
    public void StartInsertionSort()
    {
        StartCoroutine(InsertionSort());
    }

    IEnumerator InsertionSort()
    {
        for(int i=0; i < arrayNum.Length;i++)
        {
            for(int j = arrayNum.Length-1; j > 0; j--)
            {
                if(arrayNum[j].mNumIndex < arrayNum[j-1].mNumIndex)
                {
                    ChangeNumPos(j, j-1);
                }
            }
            yield return new WaitForSeconds(1.0f);
        }
    }


    #endregion

    #region 希尔排序 (nlog2n)
    public void StartShellSort()
    {
        StartCoroutine(ShellSort());
    }

    IEnumerator ShellSort()
    {
        int grap = arrayNum.Length / 2;
        while(grap > 0)
        {
            for (int i = grap; i < arrayNum.Length; i++)
            {
                int temp = arrayNum[i].mNumIndex;
                int pteIndex = i - grap;
                while (pteIndex >= 0 && arrayNum[pteIndex].mNumIndex > temp)
                {
                    ChangeNumPos(pteIndex + grap,pteIndex);
                    pteIndex -= grap;
                    yield return new WaitForSeconds(1.0f);
                }

                arrayNum[pteIndex + grap].mNumIndex = temp;
                arrayNum[pteIndex + grap].UpdateText();
            }
            grap = grap / 2;
        }
    }
    #endregion

    #region 归并排序
    public void StartMergeSort()
    {
         MergeSort();
    }

    void MergeSort()
    {
        int[] newTempArray = new int[arrayNum.Length];
        int[] numArray = new int[arrayNum.Length];
        for (int i = 0; i < arrayNum.Length; i++)
        {
            numArray[i] = arrayNum[i].mNumIndex;
        }

        Sort(numArray, newTempArray, 0, arrayNum.Length - 1);
    }

    void Sort(int[] array,int[] tempArray ,int L,int R)
    {
        if (L >= R )
        {
            return;
        }
        int mid = L + ((R - L) >> 1);
        Sort(array,tempArray,L,mid);
        Sort(array,tempArray,mid+1,R);
        Merge(array, tempArray, L, mid, R);
    }

    void Merge(int[] array,int[] tempArray,int L, int mid, int R)
    {
        int i = L;
        int j = mid+1;
        int k = 0;
        while(i<=mid&&j<=R)
        {
            tempArray[k++] = array[i] < array[j] ? array[i++] : array[j++];
        }

        while (i <= mid)
        {
            tempArray[k++] = array[i++];
        }

        while (j <= R)
        {
            tempArray[k++] = array[j++];
        }

        for (int index = 0; index < k; index++)
        {
            array[index + L] = tempArray[index];
        }
        
        for (int v = 0; v < array.Length; v++)
        {
            arrayNum[v].mNumIndex = array[v];
            arrayNum[v].UpdateText();
        }
    }
    #endregion

    #region 快速排序

    public void StartQuickSort()
    {
        Sort(0, arrayNum.Length - 1);
    }

    void Sort(int L, int R)
    {
        if (L >= R)
        {
            return;
        }
        
        int mid = QuickSort(L, R);
        Sort(L, mid);
        Sort(mid+1,R);
    }

    int QuickSort(int L, int R)
    {
        int low = L;
        int mid = arrayNum[low].mNumIndex;
        int i = L;
        int j = R;

        while (i <= j)
        {
            if (arrayNum[i].mNumIndex < mid)
            {
                ChangeNumPos(i++,low++);
            }
            else if (arrayNum[i].mNumIndex > mid)
            {
                ChangeNumPos(j--,i);
            }
            else
            {
                i++;
            }
            
        }

        return low;
    }
    
    #endregion

    #region 堆排序 
    
    //大顶堆 arr[i] >= arr[2i+1] && arr[i] >= arr[2i+2]
    private int[] _arrayTemp = null;
    public void StartHeapSort()
    {
        _arrayTemp = new int[arrayNum.Length];
        for (int i = 0; i < arrayNum.Length; i++)
        {
            _arrayTemp[i] = arrayNum[i].mNumIndex;
        }
        
        BuildMaxHeap(arrayNum.Length);
        
        for (int j = arrayNum.Length - 1; j >= 0; j--)
        {
            Switch(0, j);
            BuildMaxHeap(j);
        }

        for (int i = 0; i < _arrayTemp.Length; i++)
        {
            arrayNum[i].mNumIndex = _arrayTemp[i];
            arrayNum[i].UpdateText();
        }
    }

    void BuildMaxHeap(int len)
    {
        for (int i = len / 2 - 1; i >= 0; i--)
        {
            SortHeap(i,len);
        }
        
    }

    void SortHeap(int index,int len)
    {
        int maxIndex = index;
        if (index * 2 + 1 < len && _arrayTemp[maxIndex] < _arrayTemp[index * 2 + 1])
        {
            maxIndex = index * 2 + 1;
        }
        if (index * 2 + 2 < len && _arrayTemp[maxIndex] < _arrayTemp[index * 2 + 2])
        {
            maxIndex = index * 2 + 2;
        }

        if (maxIndex != index)
        {
            Switch(maxIndex, index);
            SortHeap(maxIndex,len);
        }
    }

    void Switch(int a, int b)
    {
        int temp = _arrayTemp[a];
        _arrayTemp[a] = _arrayTemp[b];
        _arrayTemp[b] = temp;
    }
    #endregion
}
