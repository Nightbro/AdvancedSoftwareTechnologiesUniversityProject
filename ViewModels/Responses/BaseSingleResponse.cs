using ViewModels.ViewModels;

namespace ViewModels.Responses
{
    public class BaseSingleResponse<T> : IBaseResponse<T> where T : IBaseViewModel
    {
        public bool isSuccess { get; set; }
        public string message { get; set; }
        public T data { get; set; }

    }
}
