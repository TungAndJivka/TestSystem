namespace TestSystem.DTO
{
    public class AnsweredQuestionDto
    {
        public string Id { get; set; }

        public string UserTestId { get; set; }
        public UserTestDto UserTest { get; set; }//NP

        public string AnswerId { get; set; }
        public AnswerDto Answer { get; set; } //NP
    }
}