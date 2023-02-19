using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagerApi.Model.Entities;

public class TaskDescEntity
{
    [Key] 
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; set; }
    public DateTime UpdateTime { get; set; }
    public int Status { get; set; }
}