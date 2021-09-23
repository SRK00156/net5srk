using Microsoft.AspNetCore.Mvc;

namespace Core_DataAccess_KalpeshSoliya.Models
{
    [ModelMetadataType(typeof(DeptMetadata))]
    public partial class Dept { }
    [ModelMetadataType(typeof(EmpMetadata))]
    public partial class Emp { }
}
