namespace AnimalShelter_FuryTales.Core.Models;

public class ResultModel<T>:BaseResult{
    public T Data { get; set; }
}