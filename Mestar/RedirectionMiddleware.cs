﻿namespace Mestar
{

    //To redirect to front project
    public class RedirectionMiddleware(RequestDelegate next)
    {

        public async Task InvokeAsync(HttpContext context)
        {
            //await Console.Out.WriteLineAsync(context.Request.Path);
            if (context.Request.Path == "/")

                context.Response.Redirect("/index.html");
            else if(!Path.HasExtension(context.Request.Path.Value)&&!context.Request.Path.Value.StartsWith("/api")) {
                context.Response.Redirect($"/index.html?route={context.Request.Path.Value.TrimStart('/')}");
            }
            else
            {
                await next(context);
            }
            //else
            //{
             
            //        await next(context);
            //        if(context.Response.StatusCode==404&&context.Response.ContentType is null)
            //         context.Response.Redirect("/index.html");


            //}




        }
    }
}
