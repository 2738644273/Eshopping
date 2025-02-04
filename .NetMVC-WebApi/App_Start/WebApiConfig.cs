﻿
using MVC卓越项目.Commons.Attribute;
using MVC卓越项目.Commons.Fillter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Filters;

namespace MVC卓越项目
{

    public static class WebApiConfig
    {
        // Web API 配置和服务注册
        public static void Register(HttpConfiguration config)
        {


            //全局删除循环引用
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize;
            config.Filters.Add(new CrosFilter());
            //全局异常处理器注册
            config.Filters.Add(new WebApiExceptionFilter());
            //开启特性路由 
            config.MapHttpAttributeRoutes();

        }
    }
}
