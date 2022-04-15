using System;

namespace MParysz.Utils {
  public interface IHeapElement<T> : IComparable<T> {
    int HeapIndex { get; set; }
  }

  public class Heap<T> where T : IHeapElement<T> {

    T[] elements;

    public int Count { get; private set; }

    public Heap(int maxHeapSize) {
      elements = new T[maxHeapSize];
    }

    public void Add(T element) {
      element.HeapIndex = Count;
      this.elements[Count] = element;
      SortUp(element);
      Count++;
    }

    public void UpdateElement(T element) {
      SortUp(element);
    }

    public T RemoveFirst() {
      T firstElement = elements[0];
      Count--;
      elements[0] = elements[Count];
      elements[0].HeapIndex = 0;
      SortDown(elements[0]);
      return firstElement;
    }

    public bool Contains(T element) {
      return Equals(elements[element.HeapIndex], element);
    }

    void SortUp(T element) {
      int parentIndex = (element.HeapIndex - 1) / 2;

      while (true) {
        T parentElement = elements[parentIndex];
        if (element.CompareTo(parentElement) > 0) {
          Swap(element, parentElement);
        } else {
          break;
        }

        parentIndex = (element.HeapIndex - 1) / 2;
      }
    }

    void SortDown(T element) {
      while (true) {
        int childIndexLeft = element.HeapIndex * 2 + 1;
        int childIndexRight = element.HeapIndex * 2 + 2;
        int swapIndex = 0;

        if (childIndexLeft < Count) {
          swapIndex = childIndexLeft;

          if (childIndexRight < Count) {
            if (elements[childIndexLeft].CompareTo(elements[childIndexRight]) < 0) {
              swapIndex = childIndexRight;
            }
          }

          if (element.CompareTo(elements[swapIndex]) < 0) {
            Swap(element, elements[swapIndex]);
          } else {
            return;
          }

        } else {
          return;
        }

      }
    }

    void Swap(T firstElement, T secondElement) {
      elements[firstElement.HeapIndex] = secondElement;
      elements[secondElement.HeapIndex] = firstElement;

      int elementAIndex = firstElement.HeapIndex;

      firstElement.HeapIndex = secondElement.HeapIndex;
      secondElement.HeapIndex = elementAIndex;
    }
  }
}
