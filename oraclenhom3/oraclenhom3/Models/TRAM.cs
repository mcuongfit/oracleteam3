namespace oraclenhom3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SYS.TRAMS")]
    public partial class TRAM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TRAM()
        {
            CHITIETTRAMS = new HashSet<CHITIETTRAM>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MATRAM { get; set; }

        [Required]
        [StringLength(26)]
        public string MANUOC { get; set; }

        [Required]
        [StringLength(29)]
        public string TENTRAM { get; set; }

        public int? KINHDO { get; set; }

        public short? VIDO { get; set; }

        public int? DOCAO { get; set; }

        [StringLength(20)]
        public string HINH { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHITIETTRAM> CHITIETTRAMS { get; set; }

        public virtual NUOC NUOC { get; set; }
    }
}
