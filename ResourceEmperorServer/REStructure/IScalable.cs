﻿namespace REStructure{
    public interface IScalable
    {
        void Increase(int value = 1);
        void Decrease(int value = 1);
        void Reset();
        IScalable Instantiate(int value = 1);
    }
}
