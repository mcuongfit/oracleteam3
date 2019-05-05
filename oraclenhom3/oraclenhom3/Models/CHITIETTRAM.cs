namespace oraclenhom3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SYS.CHITIETTRAMS")]
    public partial class CHITIETTRAM
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MATRAM { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short DA { get; set; }

        [Key]
        [Column(Order = 2)]
        public byte MO { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short YEAR { get; set; }

        public byte? NHIETDO { get; set; }

        public short? APSUAT { get; set; }

        public short? TOCDOGIO { get; set; }

        public short? TMAX { get; set; }

        public short? TMIN { get; set; }

        public short? LUONGMUA { get; set; }

        public virtual TRAM TRAM { get; set; }
    }
}
