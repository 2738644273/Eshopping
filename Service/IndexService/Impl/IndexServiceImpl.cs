﻿using Commons.BaseModels;
using Commons.Constant;
using Commons.Enum;
using Mapper;
using MVC卓越项目.Commons.Utils;
using Service.Service;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class IndexServiceImpl : IIndexService
    {
        public List<system_group_data> GetDataByShopConstants(string shopConstants)
        {
            using (var ctx = new eshoppingEntities())
            {
                return ctx.system_group_data.Where(e => e.group_name == shopConstants && e.is_del==false).ToList();
            }
        }

        public PageModel GetList(int page, int limit, ProductEnum flag)
        {
            using (var db = new eshoppingEntities())
            {
                PageUtils<store_product> pageUtils = new PageUtils<store_product>(1,1000);
                if (ProductEnum.TYPE_1 == flag)
                {
                    //精品推荐
                    return pageUtils.StartPage(db.store_product.Where(e => e.is_best == true && e.is_del == false).OrderBy(e => e.update_time));
                }
                else if (ProductEnum.TYPE_2 == flag)
                {
                    //热销
                    return pageUtils.StartPage(db.store_product.Where(e => e.is_hot == true && e.is_del == false).OrderBy(e => e.update_time));
                }
                else if (ProductEnum.TYPE_3 == flag)
                {
                    //新品推荐
                    return pageUtils.StartPage(db.store_product.Where(e => e.is_new == true && e.is_del == false).OrderBy(e => e.update_time));
                }
                else if (ProductEnum.TYPE_4 == flag)
                {
                    //猜你喜欢
                    return pageUtils.StartPage(db.store_product.Where(e => e.is_benefit == true && e.is_del == false).OrderBy(e => e.update_time));
                }
                return null;
            }
        }
    }
}
