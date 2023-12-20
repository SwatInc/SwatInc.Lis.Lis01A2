using System;
using System.Collections.Generic;
using System.Text;

namespace SwatInc.Lis.Lis02A2
{
    public class CommentRecord : AbstractLisRecord
    {
        [LisRecordField(4)]
        public string Text { get; set; }
        [LisRecordField(5)]
        public string Type { get; set; }

        public override string ToLISString()
        {
            return "C" + new string(LISDelimiters.FieldDelimiter, 1) + base.ToLISString();
        }

        public CommentRecord(string aLisString)
            : base(aLisString)
        {
        }

        public CommentRecord()
        {
        }
    }
}
