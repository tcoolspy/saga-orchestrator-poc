using saga_orchestrator_console.Models.Saga;
using SagaPattern.Core.Models;

namespace saga_orchestrator_console;

public class SagaOrchestrator
{
    private readonly List<ISagaStep> _steps = new();

    public void AddStep(AddNewBlogSagaStep step) => _steps.Add(step);

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