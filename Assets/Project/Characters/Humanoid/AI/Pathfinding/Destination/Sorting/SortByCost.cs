using System;
using System.Collections.Generic;

public class SortByCost : IDestinationSorter{

    int IComparer<int>.Compare(int x, int y)
    {
        return -(x - y);
    }
}

