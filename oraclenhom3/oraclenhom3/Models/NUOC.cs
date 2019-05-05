namespace oraclenhom3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SYS.NUOCS")]
    public partial class NUOC
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NUOC()
        {
            TRAMS = new HashSet<TRAM>();
        }

        [Key]
        [StringLength(26)]
        public string MANUOC { get; set; }

        [Required]
        [StringLength(20)]
        public string TENNUOC { get; set; }

        [StringLength(20)]
        public string HINH { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TRAM> TRAMS { get; set; }
    }
}
