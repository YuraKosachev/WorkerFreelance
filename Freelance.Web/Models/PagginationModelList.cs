using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList.Mvc;
using PagedList;

namespace Freelance.Web.Models
{
    public class PagginationModelList<TViewModel>
    {

        public IndexState IndexState { get; set; }
        public IPagedList<TViewModel> PageList { get; set; }
        public PagginationModelList(IndexState indexState, IPagedList<TViewModel> pageList)
        {
            IndexState = indexState;
            PageList = pageList;
        }

    }
}