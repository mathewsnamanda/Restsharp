using System;
using System.Collections.Generic;
using System.Linq;

namespace BandApi.helpers{
    public class pagelist<T>:List<T>
    {
       public int CurrentPage { get; set; }
       public int TotalPages { get; set; }
       public int PageSize { get; set; }
       public int TotalCount { get; set; }

       public bool HasPrevious=>(CurrentPage>1);
       public bool HasNext=>(CurrentPage<TotalPages);

       public pagelist(List<T> items,int totalcount,int currentpage,int pagesize)
       {

          TotalCount=totalcount;
           CurrentPage=currentpage;
           PageSize=pagesize;
           TotalPages=(int)Math.Ceiling(totalcount/(double)pagesize);
           AddRange(items);

       }
       public static pagelist<T> Create(IQueryable<T> source,int pagenumber,int pagesize)
       {
           var count=source.Count();
           var items=source.Skip((pagenumber-1)*pagesize).Take(pagesize).ToList();
         return new pagelist<T>(items,count,pagenumber,pagesize);
       }
    }
}