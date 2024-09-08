using Mono.Cecil;

public partial class ModuleWeaver
{
    // Task
    TypeDefinition taskDef;
    TypeReference genericTaskType;
    MethodReference taskConfigureAwaitMethod;
    MethodDefinition genericTaskConfigureAwaitMethodDef;
    TypeReference configuredTaskAwaitableTypeRef;
    TypeReference configuredTaskAwaiterTypeRef;
    TypeDefinition genericConfiguredTaskAwaiterTypeDef;
    TypeDefinition genericConfiguredTaskAwaitableTypeDef;
    TypeDefinition configuredTaskAwaitableTypeDef;
    TypeDefinition configuredTaskAwaiterTypeDef;
    TypeReference genericConfiguredTaskAwaiterTypeRef;
    TypeReference genericConfiguredTaskAwaitableTypeRef;

    private TypeDefinition _myCustomAwaitableTypeDef;
    private TypeDefinition _myCustomAwaiterTypeDef;
    private TypeReference _myCustomAwaitableTypeRef;
    private TypeReference _myCustomAwaiterTypeRef;
    private MethodReference _createCustomAwaitableMethodRef;

    // ValueTask
    TypeDefinition valueTaskDef;
    TypeReference genericValueTaskType;
    MethodReference valueTaskConfigureAwaitMethod;
    MethodDefinition genericValueTaskConfigureAwaitMethodDef;
    TypeReference configuredValueTaskAwaitableTypeRef;
    TypeReference configuredValueTaskAwaiterTypeRef;
    TypeDefinition genericConfiguredValueTaskAwaiterTypeDef;
    TypeDefinition genericConfiguredValueTaskAwaitableTypeDef;
    TypeDefinition configuredValueTaskAwaitableTypeDef;
    TypeDefinition configuredValueTaskAwaiterTypeDef;
    TypeReference genericConfiguredValueTaskAwaiterTypeRef;
    TypeReference genericConfiguredValueTaskAwaitableTypeRef;

    void FindTypes()
    {
        taskDef = FindTypeDefinition("System.Threading.Tasks.Task");
        var configureTaskAwaitMethodDef = taskDef.Methods.First(_ => _.Name == "ConfigureAwait");
        taskConfigureAwaitMethod = ModuleDefinition.ImportReference(configureTaskAwaitMethodDef);
        configuredTaskAwaitableTypeDef = FindTypeDefinition("System.Runtime.CompilerServices.ConfiguredTaskAwaitable");
        configuredTaskAwaiterTypeDef = configuredTaskAwaitableTypeDef.NestedTypes[0];
        configuredTaskAwaitableTypeRef = ModuleDefinition.ImportReference(configuredTaskAwaitableTypeDef);
        configuredTaskAwaiterTypeRef = ModuleDefinition.ImportReference(configuredTaskAwaiterTypeDef);

        var genericTaskDef = FindTypeDefinition("System.Threading.Tasks.Task`1");
        genericTaskConfigureAwaitMethodDef = genericTaskDef.Methods.First(_ => _.Name == "ConfigureAwait");
        genericConfiguredTaskAwaitableTypeDef = FindTypeDefinition("System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1");
        genericConfiguredTaskAwaiterTypeDef = genericConfiguredTaskAwaitableTypeDef.NestedTypes[0];
        genericConfiguredTaskAwaiterTypeRef = ModuleDefinition.ImportReference(genericConfiguredTaskAwaiterTypeDef);
        genericConfiguredTaskAwaitableTypeRef = ModuleDefinition.ImportReference(genericConfiguredTaskAwaitableTypeDef);
        genericTaskType = ModuleDefinition.ImportReference(genericTaskDef);

        if (TryFindTypeDefinition("System.Threading.Tasks.ValueTask", out valueTaskDef))
        {
            var configureValueTaskAwaitMethodDef = valueTaskDef.Methods.First(_ => _.Name == "ConfigureAwait");
            valueTaskConfigureAwaitMethod = ModuleDefinition.ImportReference(configureValueTaskAwaitMethodDef);
            configuredValueTaskAwaitableTypeDef = FindTypeDefinition("System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable");
            configuredValueTaskAwaiterTypeDef = configuredValueTaskAwaitableTypeDef.NestedTypes[0];
            configuredValueTaskAwaitableTypeRef = ModuleDefinition.ImportReference(configuredValueTaskAwaitableTypeDef);
            configuredValueTaskAwaiterTypeRef = ModuleDefinition.ImportReference(configuredValueTaskAwaiterTypeDef);
        }

        if (TryFindTypeDefinition("System.Threading.Tasks.ValueTask`1", out var genericValueTaskDef))
        {
            genericValueTaskConfigureAwaitMethodDef = genericValueTaskDef.Methods.First(_ => _.Name == "ConfigureAwait");
            genericConfiguredValueTaskAwaitableTypeDef = FindTypeDefinition("System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable`1");
            genericConfiguredValueTaskAwaiterTypeDef = genericConfiguredValueTaskAwaitableTypeDef.NestedTypes[0];
            genericConfiguredValueTaskAwaiterTypeRef = ModuleDefinition.ImportReference(genericConfiguredValueTaskAwaiterTypeDef);
            genericConfiguredValueTaskAwaitableTypeRef = ModuleDefinition.ImportReference(genericConfiguredValueTaskAwaitableTypeDef);
            genericValueTaskType = ModuleDefinition.ImportReference(genericValueTaskDef);
        }


        var extensionsTypeDef = FindTypeDefinition("Fody.MyTaskExtensions");
        var createCustomAwaitableMethodDef = extensionsTypeDef.Methods.First(_ => _.Name == "CreateCustomAwaitable");
        _createCustomAwaitableMethodRef = ModuleDefinition.ImportReference(createCustomAwaitableMethodDef);
        _myCustomAwaitableTypeDef = FindTypeDefinition("Fody.MyCustomAwaitable");
        _myCustomAwaitableTypeRef = ModuleDefinition.ImportReference(_myCustomAwaitableTypeDef);
        _myCustomAwaiterTypeDef = _myCustomAwaitableTypeDef.NestedTypes[0];
        _myCustomAwaiterTypeRef = ModuleDefinition.ImportReference(_myCustomAwaiterTypeDef);
    }
}