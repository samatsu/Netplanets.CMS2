using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netplanetes.CMS.UpdateSearchIndex
{
    class IndexOption
    {
        public static readonly string OperationParam = "/mode:";
        public static readonly string UpdateSinceParam = "/updateSince:";
        private Mode _operation = Mode.Update;
        /// <summary>
        /// オペレーションモード
        /// </summary>
        public Mode Operation
        {
            get
            {
                return _operation;
            }
            set
            {
                _operation = value;
            }
        }
        private DateTime _updateSince = DateTime.Today.AddDays(-2);
        /// <summary>
        /// 更新対象の日時。この日時以降に更新された記事のインデックスを作成する
        /// デフォルトは2日前以降に更新された記事を作成する
        /// </summary>
        public DateTime UpdateSince
        {
            get
            {
                return _updateSince;
            }
            set
            {
                _updateSince = value;
            }
        }
        public void InitilalizeWithCommandLine()
        {
            string[] commandLines = Environment.GetCommandLineArgs();

            foreach (var p in commandLines)
            {
                if (p.StartsWith(OperationParam))
                {
                    string v = p.Substring(OperationParam.Length);
                    Operation = (Mode) Enum.Parse(typeof(Mode), v, true);

                }else if (p.StartsWith(UpdateSinceParam))
                {
                    string v = p.Substring(UpdateSinceParam.Length);
                    UpdateSince = DateTime.Parse(v);
                }
            }
        }


        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("{0}以降に更新された記事を対象に{1}を行います", UpdateSince, Operation);
            return builder.ToString();
        }

    }

    enum Mode
    {
        Update,
        Rebuild,
    }
}
