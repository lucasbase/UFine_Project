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
    
    public partial class emp
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public emp()
        {
            this.order_model = new HashSet<order_model>();
            this.order_model1 = new HashSet<order_model>();
            this.relation_emp_menu = new HashSet<relation_emp_menu>();
            this.store_log = new HashSet<store_log>();
        }
    
        public int emp_id { get; set; }
        public Nullable<int> role_id { get; set; }
        public string emp_name { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string tel { get; set; }
        public Nullable<bool> gender { get; set; }
        public string address { get; set; }
        public Nullable<System.DateTime> birthday { get; set; }
        public string password { get; set; }
        public Nullable<bool> IsFrozen { get; set; }
    
        public virtual role role { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<order_model> order_model { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<order_model> order_model1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<relation_emp_menu> relation_emp_menu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<store_log> store_log { get; set; }
    }
}
