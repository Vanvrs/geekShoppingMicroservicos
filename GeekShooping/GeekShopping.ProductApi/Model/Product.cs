using GeekShopping.ProductApi.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.ProductApi.Model;

[Table("product")]//define a tabela no db
public class Product : BaseEntity
    //define os atributos
{
    [Column("name")]
    [Required]
    [StringLength(150)]
    public string Name { get; set; }

    [Column("price", TypeName = "decimal(18,2)")]
    [Required]
    [Range(1, 10000)]

    public decimal Price { get; set; }

    [Column("description")]
    [StringLength(500)]
    public string Description { get; set; }

    [Column("categoy_name)")]
    [StringLength(50)]
    public string CategoryName { get; set; }

    [Column("image_url")]
    [StringLength(200)]
    public string ImageUrl { get; set; }

}
