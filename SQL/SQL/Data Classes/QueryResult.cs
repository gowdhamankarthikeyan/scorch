using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;

namespace SQL
{
    [ActivityData("Query Result")]
    public class QueryResult
    {
        internal QueryResult()
        {
        }
        internal void setField(int index, string value)
        {
            switch (index)
            {
                case 0:
                    field00 = value;
                    break;
                case 1:
                    field01 = value;
                    break;
                case 2:
                    field02 = value;
                    break;
                case 3:
                    field03 = value;
                    break;
                case 4:
                    field04 = value;
                    break;
                case 5:
                    field05 = value;
                    break;
                case 6:
                    field06 = value;
                    break;
                case 7:
                    field07 = value;
                    break;
                case 8:
                    field08 = value;
                    break;
                case 9:
                    field09 = value;
                    break;
                case 10:
                    field10 = value;
                    break;
                case 11:
                    field11 = value;
                    break;
                case 12:
                    field12 = value;
                    break;
                case 13:
                    field13 = value;
                    break;
                case 14:
                    field14 = value;
                    break;
                case 15:
                    field15 = value;
                    break;
                case 16:
                    field16 = value;
                    break;
                case 17:
                    field17 = value;
                    break;
                case 18:
                    field18 = value;
                    break;
                case 19:
                    field19 = value;
                    break;
                case 20:
                    field20 = value;
                    break;
                case 21:
                    field21 = value;
                    break;
                case 22:
                    field22 = value;
                    break;
                case 23:
                    field23 = value;
                    break;
                case 24:
                    field24 = value;
                    break;
                case 25:
                    field25 = value;
                    break;
                case 26:
                    field26 = value;
                    break;
                case 27:
                    field27 = value;
                    break;
                case 28:
                    field28 = value;
                    break;
                case 29:
                    field29 = value;
                    break;
                case 30:
                    field30 = value;
                    break;
                case 31:
                    field31 = value;
                    break;
                case 32:
                    field32 = value;
                    break;
                case 33:
                    field33 = value;
                    break;
                case 34:
                    field34 = value;
                    break;
                case 35:
                    field35 = value;
                    break;
                case 36:
                    field36 = value;
                    break;
                case 37:
                    field37 = value;
                    break;
                case 38:
                    field38 = value;
                    break;
                case 39:
                    field39 = value;
                    break;
                case 40:
                    field40 = value;
                    break;
                case 41:
                    field41 = value;
                    break;
                case 42:
                    field42 = value;
                    break;
                case 43:
                    field43 = value;
                    break;
                case 44:
                    field44 = value;
                    break;
                case 45:
                    field45 = value;
                    break;
                case 46:
                    field46 = value;
                    break;
                case 47:
                    field47 = value;
                    break;
                case 48:
                    field48 = value;
                    break;
                case 49:
                    field49 = value;
                    break;
                case 50:
                    field50 = value;
                    break;
            }
        }
        [ActivityOutput, ActivityFilter]
        public String QueryResultString
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String field00
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String field01
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String field02
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String field03
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String field04
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String field05
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String field06
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String field07
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String field08
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String field09
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String field10
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String field11
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String field12
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String field13
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String field14
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String field15
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String field16
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String field17
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String field18
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String field19
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String field20
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String field21
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String field22
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String field23
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String field24
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String field25
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String field26
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String field27
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String field28
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String field29
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String field30
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String field31
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String field32
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String field33
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String field34
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String field35
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String field36
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String field37
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String field38
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String field39
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String field40
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String field41
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String field42
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String field43
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String field44
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String field45
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String field46
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String field47
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String field48
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String field49
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String field50
        {
            get;
            set;
        }
    }
}
