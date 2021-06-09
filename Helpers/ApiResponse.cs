using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FraktonProject.Helpers
{
    public class ApiResponse<T>
    {
        public IList<T> Data { get; set; }
    }
}
