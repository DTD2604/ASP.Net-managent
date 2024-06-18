namespace StudentManager.Models.ViewModels
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Slug { get; set; } = null!;

        public DateOnly? DateBeginning { get; set; }

        public byte Status { get; set; }

        public string? Logo { get; set; }

        public int Leader_Id { get; set; }

        public string LeaderName { get; set;}
    }
}
