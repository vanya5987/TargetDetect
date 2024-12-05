namespace TestTask.Interfaces
{
    internal interface ICombineFilterApplyer
    {
        Bitmap ApplyCombineFilter(Bitmap input, int threshold, int kernelSize);
    }
}
