namespace GMPP.MainApi.Models.Dtos
{
    /// <summary>
    /// Data transfer object Откликов
    /// </summary>
    public class JobPostingDto
    {

        public string Id { get; set; }

        public string UserId { get; set; }

        public string IdVacancy { get; set; }

        public DateTime CreateDate { get; set; }

        public string TextResponsd { get; set; }

        public StateJobPosting State { get; set; }

    }
}
