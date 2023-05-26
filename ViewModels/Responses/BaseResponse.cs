using System.Collections.Generic;
using ViewModels.ViewModels;

namespace ViewModels.Responses
{
    public class BaseResponse<T> : IBaseResponse<T> where T : IBaseViewModel
    {
        public bool isSuccess { get; set; }
        public string message { get; set; }
        public IEnumerable<T> data { get; set; }

    }
}
