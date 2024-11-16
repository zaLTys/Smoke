namespace Domain.Entities.Scenarios
{
    public class ScenarioStepResult
    {
        public Guid StepId { get; set; }
        public object Response { get; set; }
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public Dictionary<string, string> OutputData { get; set; }

        public ScenarioStepResult(Guid stepId, object response, bool isSuccess, string errorMessage, Dictionary<string, string> outputData)
        {
            StepId = stepId;
            Response = response;
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
            OutputData = outputData;
        }
    }

}
