using Omu.ValueInjecter;
using Omu.ValueInjecter.Injections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;


namespace Netplanetes.CMS.Web.Infrastracture
{
    /// <summary>
    /// マッピングを無視するプロパティを指定できる以外は、
    /// SameNameTypeと同じ動作を行うInjection.
    /// </summary>
    public class IgnoreSupportSameNameTypeInjection : ValueInjection
    {
        /// <summary>
        /// 値とキーが同じプロパティ名のクラス
        /// </summary>
        protected IDictionary<string, string> _ignoreProperties;
        public IgnoreSupportSameNameTypeInjection(params string[] propeties)
        {
            Initialize(propeties);
        }

        protected void Initialize(IEnumerable<string> properties)
        {
            _ignoreProperties = new Dictionary<string, string>();

            foreach (var item in properties)
            {
                if (!_ignoreProperties.ContainsKey(item))
                {
                    _ignoreProperties.Add(item, item);
                }
            }
        }
        /// <summary>
        /// ソースとターゲットのプロパティで型と名前が一致すれば値の注入を行う。
        /// ただし、指定された名前のプロパティはマッピングから除外する。
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        protected override void Inject(object source, object target)
        {
            var sourceProps = source.GetProps();
            foreach (var s in sourceProps)
            {
                Execute(s, source, target);
            }

        }
        protected virtual void Execute(PropertyInfo sp, object source, object target)
        {
            if (sp.CanRead && (_ignoreProperties == null || !_ignoreProperties.ContainsKey(sp.Name)))
            {
                var tp = target.GetType().GetProperty(sp.Name);
                if (tp != null && tp.CanWrite && tp.PropertyType == sp.PropertyType)
                {
                    tp.SetValue(target, sp.GetValue(source));
                }
            }
        }


    }
    /// <summary>
    /// IgnoreSupportSameNameTypeInjectionで、無視するプロパティの
    /// 名前をラムダ式で指定できるようにしたクラス
    /// 
    /// マッピング条件が同じ名前と型のプロパティなのでTにはソース、
    /// ターゲットどちらのクラスも指定可能。
    /// </summary>
    /// <typeparam name="TTarget">ターゲットオブジェクトの型</typeparam>
    public class IgnoreSupportSameNameTypeInjection<T> : IgnoreSupportSameNameTypeInjection
    {
        public IgnoreSupportSameNameTypeInjection(params Expression<Func<T, object>>[] nodes)
        {
            List<string> properties = new List<string>();

            ListPropertiesVisitor<T> visitor = new ListPropertiesVisitor<T>();
            visitor.Translate(nodes);
            Initialize(visitor.ListedProperties);
        }
    }
    /// <summary>
    /// プロパティアクセスされるプロパティの一覧を列挙するビジター
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ListPropertiesVisitor<T> : ExpressionVisitor
    {
        private List<string> _properties;

        public IEnumerable<string> ListedProperties
        {
            get { return _properties.AsReadOnly(); }
        }

        public ListPropertiesVisitor() { }

        public IEnumerable<string> Translate(params Expression<Func<T, object>>[] nodes)
        {
            _properties = new List<string>();
            foreach (var item in nodes)
            {
                Visit(item);
            }
            return _properties;
        }
        protected override Expression VisitMember(MemberExpression node)
        {
            _properties.Add(node.Member.Name);
            return node;
        }
    }

}