//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Youfan_Invoicing_Management_System.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class relation_role_menu
    {
        public int relation_id { get; set; }
        public Nullable<int> role_id { get; set; }
        public Nullable<int> menu_id { get; set; }
    
        public virtual menu menu { get; set; }
        public virtual role role { get; set; }
    }
}
