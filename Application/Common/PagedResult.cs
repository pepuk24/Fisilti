using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    public class PagedResult<T> : Result<IEnumerable<T>>
    {
        //DTO: Data Transfer Object, biz kullanıcıya verileri gönderirken içerisinde kullanıcının işine yaramayacak verilerinde bulunduğu Entitynin kendisini göndermek yerine Entitynin törpülenmiş yani filtrelenmiş versiyonu olan içerisinde sadece kullanıcının ihtiyaç duyabileceği verileri barındıran classlarımız.
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public static PagedResult<T> Create(IEnumerable<T> data, int totalCount, int pageIndex, int pageSize) 
        {
            return new PagedResult<T>
            {
                Success = true,
                Data = data,
                TotalCount = totalCount,
                PageIndex = pageIndex,
                PageSize = pageSize
            };

        }
    }
}
