using Netplanetes.CMS.Web.Models.Content;
using Netplanetes.CMS.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel.Channels;
using System.Web.Http;

namespace Netplanetes.CMS.Web.Controllers
{
    /// <summary>
    /// 外部に公開するAPIコントローラー
    /// </summary>
    [RoutePrefix("v1/client")]
    public class ClientController : ApiController
    {
        IContentService _service;
        public ClientController(IContentService service)
        {
            _service = service;
        }

        /// <summary>
        /// 記事IDからコメントの一覧を取得する
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        [AcceptVerbs("POST")]
        [Route("comments/{articleId}")]
        public IEnumerable<CommentViewModel> Comments(int articleId)
        {
            return _service.RetrieveCommentList(articleId);
        }
        [AcceptVerbs("POST")]
        [Route("comment/")]
        public IEnumerable<CommentViewModel> Comment(CommentPostViewModel model)
        {
            if (ModelState.IsValid)
            {
                _service.AddComment(model, GetClientIp());
                return  _service.RetrieveCommentList(model.ArticleID);
            }
            throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState));
        }
        // GET api/<controller> 
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        private string GetClientIp(HttpRequestMessage request = null)
        {
            request = request ?? this.Request;

            if (request.Properties.ContainsKey("MS_HttpContext"))
            {
                return ((System.Web.HttpContextWrapper)request.Properties["MS_HttpContext"]).Request.UserHostAddress;
            }
            else if (request.Properties.ContainsKey(RemoteEndpointMessageProperty.Name))
            {
                RemoteEndpointMessageProperty prop = (RemoteEndpointMessageProperty)request.Properties[RemoteEndpointMessageProperty.Name];
                return prop.Address;
            }
            else if (System.Web.HttpContext.Current != null)
            {
                return System.Web.HttpContext.Current.Request.UserHostAddress;
            }
            else
            {
                return null;
            }
        }



        #region comment
        //// GET api/<controller>/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<controller>
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/<controller>/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/<controller>/5
        //public void Delete(int id)
        //{
        //}
        #endregion
    }
}