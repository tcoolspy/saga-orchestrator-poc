using SagaPattern.Core.Models;

namespace saga_orchestrator;

public class SagaOrchestrator
{
    private readonly List<ISagaStep> _steps = new();
    
    public void AddStep(ISagaStep step) => _steps.Add(step);

    public async Task ExecuteSagaAsync<T>(T entity)
    {
        var executedSteps = new Stack<ISagaStep>();

        try 
        {
            foreach (var step in _steps)
            {
                await step.ExecuteAsync(entity);
                executedSteps.Push(step);
            }
        }
        catch (Exception e)
        {
            while (executedSteps.Count > 0)
            {
                var step = executedSteps.Pop();
                await step.CompensateAsync(entity);
            }
        }
    }
}