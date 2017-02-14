#if !EXCLUDE_CODEGEN
#pragma warning disable 162
#pragma warning disable 219
#pragma warning disable 414
#pragma warning disable 649
#pragma warning disable 693
#pragma warning disable 1591
#pragma warning disable 1998
[assembly: global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.3.1.0")]
[assembly: global::Orleans.CodeGeneration.OrleansCodeGenerationTargetAttribute("Minecraft.Orleans.Interface, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null")]
namespace Minecraft.Orleans.Interface
{
    using global::Orleans.Async;
    using global::Orleans;
    using global::System.Reflection;

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.3.1.0"), global::System.SerializableAttribute, global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.GrainReferenceAttribute(typeof (global::Minecraft.Orleans.Interface.IGrain1))]
    internal class OrleansCodeGenGrain1Reference : global::Orleans.Runtime.GrainReference, global::Minecraft.Orleans.Interface.IGrain1
    {
        protected @OrleansCodeGenGrain1Reference(global::Orleans.Runtime.GrainReference @other): base (@other)
        {
        }

        protected @OrleansCodeGenGrain1Reference(global::System.Runtime.Serialization.SerializationInfo @info, global::System.Runtime.Serialization.StreamingContext @context): base (@info, @context)
        {
        }

        protected override global::System.Int32 InterfaceId
        {
            get
            {
                return 243816520;
            }
        }

        public override global::System.String InterfaceName
        {
            get
            {
                return "global::Minecraft.Orleans.Interface.IGrain1";
            }
        }

        public override global::System.Boolean @IsCompatible(global::System.Int32 @interfaceId)
        {
            return @interfaceId == 243816520;
        }

        protected override global::System.String @GetMethodName(global::System.Int32 @interfaceId, global::System.Int32 @methodId)
        {
            switch (@interfaceId)
            {
                case 243816520:
                    switch (@methodId)
                    {
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + 243816520 + ",methodId=" + @methodId);
                    }

                default:
                    throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
            }
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.3.1.0"), global::Orleans.CodeGeneration.MethodInvokerAttribute("global::Minecraft.Orleans.Interface.IGrain1", 243816520, typeof (global::Minecraft.Orleans.Interface.IGrain1)), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    internal class OrleansCodeGenGrain1MethodInvoker : global::Orleans.CodeGeneration.IGrainMethodInvoker
    {
        public global::System.Threading.Tasks.Task<global::System.Object> @Invoke(global::Orleans.Runtime.IAddressable @grain, global::Orleans.CodeGeneration.InvokeMethodRequest @request)
        {
            global::System.Int32 interfaceId = @request.@InterfaceId;
            global::System.Int32 methodId = @request.@MethodId;
            global::System.Object[] arguments = @request.@Arguments;
            if (@grain == null)
                throw new global::System.ArgumentNullException("grain");
            switch (interfaceId)
            {
                case 243816520:
                    switch (methodId)
                    {
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + 243816520 + ",methodId=" + methodId);
                    }

                default:
                    throw new global::System.NotImplementedException("interfaceId=" + interfaceId);
            }
        }

        public global::System.Int32 InterfaceId
        {
            get
            {
                return 243816520;
            }
        }
    }
}

namespace Minecraft.OrleansInterfaces.Grains
{
    using global::Orleans.Async;
    using global::Orleans;
    using global::System.Reflection;

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.3.1.0"), global::System.SerializableAttribute, global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.GrainReferenceAttribute(typeof (global::Minecraft.OrleansInterfaces.Grains.IChunkGrain))]
    internal class OrleansCodeGenChunkGrainReference : global::Orleans.Runtime.GrainReference, global::Minecraft.OrleansInterfaces.Grains.IChunkGrain
    {
        protected @OrleansCodeGenChunkGrainReference(global::Orleans.Runtime.GrainReference @other): base (@other)
        {
        }

        protected @OrleansCodeGenChunkGrainReference(global::System.Runtime.Serialization.SerializationInfo @info, global::System.Runtime.Serialization.StreamingContext @context): base (@info, @context)
        {
        }

        protected override global::System.Int32 InterfaceId
        {
            get
            {
                return -190298963;
            }
        }

        public override global::System.String InterfaceName
        {
            get
            {
                return "global::Minecraft.OrleansInterfaces.Grains.IChunkGrain";
            }
        }

        public override global::System.Boolean @IsCompatible(global::System.Int32 @interfaceId)
        {
            return @interfaceId == -190298963 || @interfaceId == -1277021679;
        }

        protected override global::System.String @GetMethodName(global::System.Int32 @interfaceId, global::System.Int32 @methodId)
        {
            switch (@interfaceId)
            {
                case -190298963:
                    switch (@methodId)
                    {
                        case 1814712465:
                            return "Initialize";
                        case -1619913246:
                            return "Associate";
                        case 1496538735:
                            return "SetResponseTime";
                        case -1011980538:
                            return "StartPlayer";
                        case -373326364:
                            return "UpdatePlayer";
                        case 975228763:
                            return "LeavePlayer";
                        case -48147871:
                            return "UpdateBlock";
                        case 1520867904:
                            return "InformOfChange";
                        case 79473936:
                            return "InspectPlayers";
                        case 154625779:
                            return "InspectBlocks";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + -190298963 + ",methodId=" + @methodId);
                    }

                case -1277021679:
                    switch (@methodId)
                    {
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + -1277021679 + ",methodId=" + @methodId);
                    }

                default:
                    throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
            }
        }

        public global::System.Threading.Tasks.Task<global::Minecraft.OrleansInterfaces.FeedbackMessage> @Initialize(global::System.String @id, global::Minecraft.OrleansInterfaces.IntVec3 @minLocation, global::Minecraft.OrleansInterfaces.IntVec3 @maxLocation)
        {
            return base.@InvokeMethodAsync<global::Minecraft.OrleansInterfaces.FeedbackMessage>(1814712465, new global::System.Object[]{@id, @minLocation, @maxLocation});
        }

        public global::System.Threading.Tasks.Task<global::Minecraft.OrleansInterfaces.FeedbackMessage> @Associate(global::System.String @id, global::System.String @grainID, global::System.Int32 @fidelity, global::Minecraft.OrleansInterfaces.IntVec3 @position, global::Minecraft.OrleansInterfaces.IntVec3 @size)
        {
            return base.@InvokeMethodAsync<global::Minecraft.OrleansInterfaces.FeedbackMessage>(-1619913246, new global::System.Object[]{@id, @grainID, @fidelity, @position, @size});
        }

        public global::System.Threading.Tasks.Task<global::Minecraft.OrleansInterfaces.FeedbackMessage> @SetResponseTime(global::System.Int32 @millisecond)
        {
            return base.@InvokeMethodAsync<global::Minecraft.OrleansInterfaces.FeedbackMessage>(1496538735, new global::System.Object[]{@millisecond});
        }

        public global::System.Threading.Tasks.Task<global::Minecraft.OrleansInterfaces.FeedbackMessage> @StartPlayer(global::System.String @playerSessionID, global::Minecraft.OrleansInterfaces.Grains.IPlayerObserver @playerObserver, global::Minecraft.OrleansInterfaces.Vec3 @pos, global::System.String[] @fromGrain)
        {
            global::Orleans.CodeGeneration.GrainFactoryBase.@CheckGrainObserverParamInternal(@playerObserver);
            return base.@InvokeMethodAsync<global::Minecraft.OrleansInterfaces.FeedbackMessage>(-1011980538, new global::System.Object[]{@playerSessionID, @playerObserver is global::Orleans.Grain ? @playerObserver.@AsReference<global::Minecraft.OrleansInterfaces.Grains.IPlayerObserver>() : @playerObserver, @pos, @fromGrain});
        }

        public global::System.Threading.Tasks.Task<global::System.String> @UpdatePlayer(global::System.String @playerSessionID, global::Minecraft.OrleansInterfaces.Grains.IPlayerObserver @playerObserver, global::Minecraft.OrleansInterfaces.Vec3 @pos)
        {
            global::Orleans.CodeGeneration.GrainFactoryBase.@CheckGrainObserverParamInternal(@playerObserver);
            return base.@InvokeMethodAsync<global::System.String>(-373326364, new global::System.Object[]{@playerSessionID, @playerObserver is global::Orleans.Grain ? @playerObserver.@AsReference<global::Minecraft.OrleansInterfaces.Grains.IPlayerObserver>() : @playerObserver, @pos});
        }

        public global::System.Threading.Tasks.Task<global::Minecraft.OrleansInterfaces.FeedbackMessage> @LeavePlayer(global::System.String @playerSessionID)
        {
            return base.@InvokeMethodAsync<global::Minecraft.OrleansInterfaces.FeedbackMessage>(975228763, new global::System.Object[]{@playerSessionID});
        }

        public global::System.Threading.Tasks.Task<global::Minecraft.OrleansInterfaces.FeedbackMessage> @UpdateBlock(global::System.String @playerSessionID, global::Minecraft.OrleansInterfaces.BlockInfo @blockUpdate)
        {
            return base.@InvokeMethodAsync<global::Minecraft.OrleansInterfaces.FeedbackMessage>(-48147871, new global::System.Object[]{@playerSessionID, @blockUpdate});
        }

        public global::System.Threading.Tasks.Task<global::Minecraft.OrleansInterfaces.ResponseToChunkUpdateMessage> @InformOfChange(global::System.Collections.Generic.List<global::Minecraft.OrleansInterfaces.Grains.IPlayerObserver> @initObservers, global::System.Collections.Generic.List<global::Minecraft.OrleansInterfaces.Grains.IPlayerObserver> @updateObservers, global::Minecraft.OrleansInterfaces.MinecraftVersion @lastPlayerVersion, global::System.Int32 @suggestedMaxPlayerRequests, global::Minecraft.OrleansInterfaces.MinecraftVersion @lastBlockVersion, global::System.Int32 @suggestedMaxBlockRequests)
        {
            return base.@InvokeMethodAsync<global::Minecraft.OrleansInterfaces.ResponseToChunkUpdateMessage>(1520867904, new global::System.Object[]{@initObservers, @updateObservers, @lastPlayerVersion, @suggestedMaxPlayerRequests, @lastBlockVersion, @suggestedMaxBlockRequests});
        }

        public global::System.Threading.Tasks.Task<global::Minecraft.OrleansInterfaces.PlayerInfo[]> @InspectPlayers()
        {
            return base.@InvokeMethodAsync<global::Minecraft.OrleansInterfaces.PlayerInfo[]>(79473936, null);
        }

        public global::System.Threading.Tasks.Task<global::Minecraft.OrleansInterfaces.BlockInfo[]> @InspectBlocks()
        {
            return base.@InvokeMethodAsync<global::Minecraft.OrleansInterfaces.BlockInfo[]>(154625779, null);
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.3.1.0"), global::Orleans.CodeGeneration.MethodInvokerAttribute("global::Minecraft.OrleansInterfaces.Grains.IChunkGrain", -190298963, typeof (global::Minecraft.OrleansInterfaces.Grains.IChunkGrain)), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    internal class OrleansCodeGenChunkGrainMethodInvoker : global::Orleans.CodeGeneration.IGrainMethodInvoker
    {
        public global::System.Threading.Tasks.Task<global::System.Object> @Invoke(global::Orleans.Runtime.IAddressable @grain, global::Orleans.CodeGeneration.InvokeMethodRequest @request)
        {
            global::System.Int32 interfaceId = @request.@InterfaceId;
            global::System.Int32 methodId = @request.@MethodId;
            global::System.Object[] arguments = @request.@Arguments;
            if (@grain == null)
                throw new global::System.ArgumentNullException("grain");
            switch (interfaceId)
            {
                case -190298963:
                    switch (methodId)
                    {
                        case 1814712465:
                            return ((global::Minecraft.OrleansInterfaces.Grains.IChunkGrain)@grain).@Initialize((global::System.String)arguments[0], (global::Minecraft.OrleansInterfaces.IntVec3)arguments[1], (global::Minecraft.OrleansInterfaces.IntVec3)arguments[2]).@Box();
                        case -1619913246:
                            return ((global::Minecraft.OrleansInterfaces.Grains.IChunkGrain)@grain).@Associate((global::System.String)arguments[0], (global::System.String)arguments[1], (global::System.Int32)arguments[2], (global::Minecraft.OrleansInterfaces.IntVec3)arguments[3], (global::Minecraft.OrleansInterfaces.IntVec3)arguments[4]).@Box();
                        case 1496538735:
                            return ((global::Minecraft.OrleansInterfaces.Grains.IChunkGrain)@grain).@SetResponseTime((global::System.Int32)arguments[0]).@Box();
                        case -1011980538:
                            return ((global::Minecraft.OrleansInterfaces.Grains.IChunkGrain)@grain).@StartPlayer((global::System.String)arguments[0], (global::Minecraft.OrleansInterfaces.Grains.IPlayerObserver)arguments[1], (global::Minecraft.OrleansInterfaces.Vec3)arguments[2], (global::System.String[])arguments[3]).@Box();
                        case -373326364:
                            return ((global::Minecraft.OrleansInterfaces.Grains.IChunkGrain)@grain).@UpdatePlayer((global::System.String)arguments[0], (global::Minecraft.OrleansInterfaces.Grains.IPlayerObserver)arguments[1], (global::Minecraft.OrleansInterfaces.Vec3)arguments[2]).@Box();
                        case 975228763:
                            return ((global::Minecraft.OrleansInterfaces.Grains.IChunkGrain)@grain).@LeavePlayer((global::System.String)arguments[0]).@Box();
                        case -48147871:
                            return ((global::Minecraft.OrleansInterfaces.Grains.IChunkGrain)@grain).@UpdateBlock((global::System.String)arguments[0], (global::Minecraft.OrleansInterfaces.BlockInfo)arguments[1]).@Box();
                        case 1520867904:
                            return ((global::Minecraft.OrleansInterfaces.Grains.IChunkGrain)@grain).@InformOfChange((global::System.Collections.Generic.List<global::Minecraft.OrleansInterfaces.Grains.IPlayerObserver>)arguments[0], (global::System.Collections.Generic.List<global::Minecraft.OrleansInterfaces.Grains.IPlayerObserver>)arguments[1], (global::Minecraft.OrleansInterfaces.MinecraftVersion)arguments[2], (global::System.Int32)arguments[3], (global::Minecraft.OrleansInterfaces.MinecraftVersion)arguments[4], (global::System.Int32)arguments[5]).@Box();
                        case 79473936:
                            return ((global::Minecraft.OrleansInterfaces.Grains.IChunkGrain)@grain).@InspectPlayers().@Box();
                        case 154625779:
                            return ((global::Minecraft.OrleansInterfaces.Grains.IChunkGrain)@grain).@InspectBlocks().@Box();
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + -190298963 + ",methodId=" + methodId);
                    }

                case -1277021679:
                    switch (methodId)
                    {
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + -1277021679 + ",methodId=" + methodId);
                    }

                default:
                    throw new global::System.NotImplementedException("interfaceId=" + interfaceId);
            }
        }

        public global::System.Int32 InterfaceId
        {
            get
            {
                return -190298963;
            }
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.3.1.0"), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.SerializerAttribute(typeof (global::Minecraft.OrleansInterfaces.FeedbackMessage)), global::Orleans.CodeGeneration.RegisterSerializerAttribute]
    internal class OrleansCodeGenMinecraft_OrleansInterfaces_FeedbackMessageSerializer
    {
        [global::Orleans.CodeGeneration.CopierMethodAttribute]
        public static global::System.Object DeepCopier(global::System.Object original)
        {
            global::Minecraft.OrleansInterfaces.FeedbackMessage input = ((global::Minecraft.OrleansInterfaces.FeedbackMessage)original);
            global::Minecraft.OrleansInterfaces.FeedbackMessage result = (global::Minecraft.OrleansInterfaces.FeedbackMessage)global::System.Runtime.Serialization.FormatterServices.@GetUninitializedObject(typeof (global::Minecraft.OrleansInterfaces.FeedbackMessage));
            global::Orleans.@Serialization.@SerializationContext.@Current.@RecordObject(original, result);
            result.@mStringValue = input.@mStringValue;
            result.@mType = input.@mType;
            return result;
        }

        [global::Orleans.CodeGeneration.SerializerMethodAttribute]
        public static void Serializer(global::System.Object untypedInput, global::Orleans.Serialization.BinaryTokenStreamWriter stream, global::System.Type expected)
        {
            global::Minecraft.OrleansInterfaces.FeedbackMessage input = (global::Minecraft.OrleansInterfaces.FeedbackMessage)untypedInput;
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@mStringValue, stream, typeof (global::System.String));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@mType, stream, typeof (global::Minecraft.OrleansInterfaces.FeedbackMessage.Responces));
        }

        [global::Orleans.CodeGeneration.DeserializerMethodAttribute]
        public static global::System.Object Deserializer(global::System.Type expected, global::Orleans.Serialization.BinaryTokenStreamReader stream)
        {
            global::Minecraft.OrleansInterfaces.FeedbackMessage result = (global::Minecraft.OrleansInterfaces.FeedbackMessage)global::System.Runtime.Serialization.FormatterServices.@GetUninitializedObject(typeof (global::Minecraft.OrleansInterfaces.FeedbackMessage));
            global::Orleans.@Serialization.@DeserializationContext.@Current.@RecordObject(result);
            result.@mStringValue = (global::System.String)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.String), stream);
            result.@mType = (global::Minecraft.OrleansInterfaces.FeedbackMessage.Responces)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::Minecraft.OrleansInterfaces.FeedbackMessage.Responces), stream);
            return (global::Minecraft.OrleansInterfaces.FeedbackMessage)result;
        }

        public static void Register()
        {
            global::Orleans.Serialization.SerializationManager.@Register(typeof (global::Minecraft.OrleansInterfaces.FeedbackMessage), DeepCopier, Serializer, Deserializer);
        }

        static OrleansCodeGenMinecraft_OrleansInterfaces_FeedbackMessageSerializer()
        {
            Register();
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.3.1.0"), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.SerializerAttribute(typeof (global::Minecraft.OrleansInterfaces.IntVec3)), global::Orleans.CodeGeneration.RegisterSerializerAttribute]
    internal class OrleansCodeGenMinecraft_OrleansInterfaces_IntVec3Serializer
    {
        private static readonly global::System.Reflection.FieldInfo field0 = typeof (global::Minecraft.OrleansInterfaces.IntVec3).@GetTypeInfo().@GetField("x", (System.@Reflection.@BindingFlags.@Instance | System.@Reflection.@BindingFlags.@NonPublic | System.@Reflection.@BindingFlags.@Public));
        private static readonly global::System.Func<global::Minecraft.OrleansInterfaces.IntVec3, global::System.Int32> getField0 = (global::System.Func<global::Minecraft.OrleansInterfaces.IntVec3, global::System.Int32>)global::Orleans.Serialization.SerializationManager.@GetGetter(field0);
        private static readonly global::System.Action<global::Minecraft.OrleansInterfaces.IntVec3, global::System.Int32> setField0 = (global::System.Action<global::Minecraft.OrleansInterfaces.IntVec3, global::System.Int32>)global::Orleans.Serialization.SerializationManager.@GetReferenceSetter(field0);
        private static readonly global::System.Reflection.FieldInfo field1 = typeof (global::Minecraft.OrleansInterfaces.IntVec3).@GetTypeInfo().@GetField("y", (System.@Reflection.@BindingFlags.@Instance | System.@Reflection.@BindingFlags.@NonPublic | System.@Reflection.@BindingFlags.@Public));
        private static readonly global::System.Func<global::Minecraft.OrleansInterfaces.IntVec3, global::System.Int32> getField1 = (global::System.Func<global::Minecraft.OrleansInterfaces.IntVec3, global::System.Int32>)global::Orleans.Serialization.SerializationManager.@GetGetter(field1);
        private static readonly global::System.Action<global::Minecraft.OrleansInterfaces.IntVec3, global::System.Int32> setField1 = (global::System.Action<global::Minecraft.OrleansInterfaces.IntVec3, global::System.Int32>)global::Orleans.Serialization.SerializationManager.@GetReferenceSetter(field1);
        private static readonly global::System.Reflection.FieldInfo field2 = typeof (global::Minecraft.OrleansInterfaces.IntVec3).@GetTypeInfo().@GetField("z", (System.@Reflection.@BindingFlags.@Instance | System.@Reflection.@BindingFlags.@NonPublic | System.@Reflection.@BindingFlags.@Public));
        private static readonly global::System.Func<global::Minecraft.OrleansInterfaces.IntVec3, global::System.Int32> getField2 = (global::System.Func<global::Minecraft.OrleansInterfaces.IntVec3, global::System.Int32>)global::Orleans.Serialization.SerializationManager.@GetGetter(field2);
        private static readonly global::System.Action<global::Minecraft.OrleansInterfaces.IntVec3, global::System.Int32> setField2 = (global::System.Action<global::Minecraft.OrleansInterfaces.IntVec3, global::System.Int32>)global::Orleans.Serialization.SerializationManager.@GetReferenceSetter(field2);
        [global::Orleans.CodeGeneration.CopierMethodAttribute]
        public static global::System.Object DeepCopier(global::System.Object original)
        {
            global::Minecraft.OrleansInterfaces.IntVec3 input = ((global::Minecraft.OrleansInterfaces.IntVec3)original);
            global::Minecraft.OrleansInterfaces.IntVec3 result = new global::Minecraft.OrleansInterfaces.IntVec3();
            global::Orleans.@Serialization.@SerializationContext.@Current.@RecordObject(original, result);
            setField0(result, getField0(input));
            setField1(result, getField1(input));
            setField2(result, getField2(input));
            return result;
        }

        [global::Orleans.CodeGeneration.SerializerMethodAttribute]
        public static void Serializer(global::System.Object untypedInput, global::Orleans.Serialization.BinaryTokenStreamWriter stream, global::System.Type expected)
        {
            global::Minecraft.OrleansInterfaces.IntVec3 input = (global::Minecraft.OrleansInterfaces.IntVec3)untypedInput;
            global::Orleans.Serialization.SerializationManager.@SerializeInner(getField0(input), stream, typeof (global::System.Int32));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(getField1(input), stream, typeof (global::System.Int32));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(getField2(input), stream, typeof (global::System.Int32));
        }

        [global::Orleans.CodeGeneration.DeserializerMethodAttribute]
        public static global::System.Object Deserializer(global::System.Type expected, global::Orleans.Serialization.BinaryTokenStreamReader stream)
        {
            global::Minecraft.OrleansInterfaces.IntVec3 result = new global::Minecraft.OrleansInterfaces.IntVec3();
            global::Orleans.@Serialization.@DeserializationContext.@Current.@RecordObject(result);
            setField0(result, (global::System.Int32)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.Int32), stream));
            setField1(result, (global::System.Int32)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.Int32), stream));
            setField2(result, (global::System.Int32)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.Int32), stream));
            return (global::Minecraft.OrleansInterfaces.IntVec3)result;
        }

        public static void Register()
        {
            global::Orleans.Serialization.SerializationManager.@Register(typeof (global::Minecraft.OrleansInterfaces.IntVec3), DeepCopier, Serializer, Deserializer);
        }

        static OrleansCodeGenMinecraft_OrleansInterfaces_IntVec3Serializer()
        {
            Register();
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.3.1.0"), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.SerializerAttribute(typeof (global::Minecraft.OrleansInterfaces.Vec3)), global::Orleans.CodeGeneration.RegisterSerializerAttribute]
    internal class OrleansCodeGenMinecraft_OrleansInterfaces_Vec3Serializer
    {
        private static readonly global::System.Reflection.FieldInfo field0 = typeof (global::Minecraft.OrleansInterfaces.Vec3).@GetTypeInfo().@GetField("x", (System.@Reflection.@BindingFlags.@Instance | System.@Reflection.@BindingFlags.@NonPublic | System.@Reflection.@BindingFlags.@Public));
        private static readonly global::System.Func<global::Minecraft.OrleansInterfaces.Vec3, global::System.Single> getField0 = (global::System.Func<global::Minecraft.OrleansInterfaces.Vec3, global::System.Single>)global::Orleans.Serialization.SerializationManager.@GetGetter(field0);
        private static readonly global::System.Action<global::Minecraft.OrleansInterfaces.Vec3, global::System.Single> setField0 = (global::System.Action<global::Minecraft.OrleansInterfaces.Vec3, global::System.Single>)global::Orleans.Serialization.SerializationManager.@GetReferenceSetter(field0);
        private static readonly global::System.Reflection.FieldInfo field1 = typeof (global::Minecraft.OrleansInterfaces.Vec3).@GetTypeInfo().@GetField("y", (System.@Reflection.@BindingFlags.@Instance | System.@Reflection.@BindingFlags.@NonPublic | System.@Reflection.@BindingFlags.@Public));
        private static readonly global::System.Func<global::Minecraft.OrleansInterfaces.Vec3, global::System.Single> getField1 = (global::System.Func<global::Minecraft.OrleansInterfaces.Vec3, global::System.Single>)global::Orleans.Serialization.SerializationManager.@GetGetter(field1);
        private static readonly global::System.Action<global::Minecraft.OrleansInterfaces.Vec3, global::System.Single> setField1 = (global::System.Action<global::Minecraft.OrleansInterfaces.Vec3, global::System.Single>)global::Orleans.Serialization.SerializationManager.@GetReferenceSetter(field1);
        private static readonly global::System.Reflection.FieldInfo field2 = typeof (global::Minecraft.OrleansInterfaces.Vec3).@GetTypeInfo().@GetField("z", (System.@Reflection.@BindingFlags.@Instance | System.@Reflection.@BindingFlags.@NonPublic | System.@Reflection.@BindingFlags.@Public));
        private static readonly global::System.Func<global::Minecraft.OrleansInterfaces.Vec3, global::System.Single> getField2 = (global::System.Func<global::Minecraft.OrleansInterfaces.Vec3, global::System.Single>)global::Orleans.Serialization.SerializationManager.@GetGetter(field2);
        private static readonly global::System.Action<global::Minecraft.OrleansInterfaces.Vec3, global::System.Single> setField2 = (global::System.Action<global::Minecraft.OrleansInterfaces.Vec3, global::System.Single>)global::Orleans.Serialization.SerializationManager.@GetReferenceSetter(field2);
        [global::Orleans.CodeGeneration.CopierMethodAttribute]
        public static global::System.Object DeepCopier(global::System.Object original)
        {
            global::Minecraft.OrleansInterfaces.Vec3 input = ((global::Minecraft.OrleansInterfaces.Vec3)original);
            global::Minecraft.OrleansInterfaces.Vec3 result = (global::Minecraft.OrleansInterfaces.Vec3)global::System.Runtime.Serialization.FormatterServices.@GetUninitializedObject(typeof (global::Minecraft.OrleansInterfaces.Vec3));
            global::Orleans.@Serialization.@SerializationContext.@Current.@RecordObject(original, result);
            setField0(result, getField0(input));
            setField1(result, getField1(input));
            setField2(result, getField2(input));
            return result;
        }

        [global::Orleans.CodeGeneration.SerializerMethodAttribute]
        public static void Serializer(global::System.Object untypedInput, global::Orleans.Serialization.BinaryTokenStreamWriter stream, global::System.Type expected)
        {
            global::Minecraft.OrleansInterfaces.Vec3 input = (global::Minecraft.OrleansInterfaces.Vec3)untypedInput;
            global::Orleans.Serialization.SerializationManager.@SerializeInner(getField0(input), stream, typeof (global::System.Single));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(getField1(input), stream, typeof (global::System.Single));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(getField2(input), stream, typeof (global::System.Single));
        }

        [global::Orleans.CodeGeneration.DeserializerMethodAttribute]
        public static global::System.Object Deserializer(global::System.Type expected, global::Orleans.Serialization.BinaryTokenStreamReader stream)
        {
            global::Minecraft.OrleansInterfaces.Vec3 result = (global::Minecraft.OrleansInterfaces.Vec3)global::System.Runtime.Serialization.FormatterServices.@GetUninitializedObject(typeof (global::Minecraft.OrleansInterfaces.Vec3));
            global::Orleans.@Serialization.@DeserializationContext.@Current.@RecordObject(result);
            setField0(result, (global::System.Single)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.Single), stream));
            setField1(result, (global::System.Single)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.Single), stream));
            setField2(result, (global::System.Single)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.Single), stream));
            return (global::Minecraft.OrleansInterfaces.Vec3)result;
        }

        public static void Register()
        {
            global::Orleans.Serialization.SerializationManager.@Register(typeof (global::Minecraft.OrleansInterfaces.Vec3), DeepCopier, Serializer, Deserializer);
        }

        static OrleansCodeGenMinecraft_OrleansInterfaces_Vec3Serializer()
        {
            Register();
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.3.1.0"), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.SerializerAttribute(typeof (global::Minecraft.OrleansInterfaces.BlockInfo)), global::Orleans.CodeGeneration.RegisterSerializerAttribute]
    internal class OrleansCodeGenMinecraft_OrleansInterfaces_BlockInfoSerializer
    {
        [global::Orleans.CodeGeneration.CopierMethodAttribute]
        public static global::System.Object DeepCopier(global::System.Object original)
        {
            return original;
        }

        [global::Orleans.CodeGeneration.SerializerMethodAttribute]
        public static void Serializer(global::System.Object untypedInput, global::Orleans.Serialization.BinaryTokenStreamWriter stream, global::System.Type expected)
        {
            global::Minecraft.OrleansInterfaces.BlockInfo input = (global::Minecraft.OrleansInterfaces.BlockInfo)untypedInput;
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@mCreatorID, stream, typeof (global::System.String));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@mData, stream, typeof (global::System.Int32));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@mDataExtended, stream, typeof (global::System.Int32));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@mID, stream, typeof (global::System.String));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@mLastTouchedID, stream, typeof (global::System.String));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@mPosition, stream, typeof (global::Minecraft.OrleansInterfaces.IntVec3));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@mVersion, stream, typeof (global::Minecraft.OrleansInterfaces.MinecraftVersion));
        }

        [global::Orleans.CodeGeneration.DeserializerMethodAttribute]
        public static global::System.Object Deserializer(global::System.Type expected, global::Orleans.Serialization.BinaryTokenStreamReader stream)
        {
            global::Minecraft.OrleansInterfaces.BlockInfo result = new global::Minecraft.OrleansInterfaces.BlockInfo();
            global::Orleans.@Serialization.@DeserializationContext.@Current.@RecordObject(result);
            result.@mCreatorID = (global::System.String)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.String), stream);
            result.@mData = (global::System.Int32)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.Int32), stream);
            result.@mDataExtended = (global::System.Int32)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.Int32), stream);
            result.@mID = (global::System.String)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.String), stream);
            result.@mLastTouchedID = (global::System.String)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.String), stream);
            result.@mPosition = (global::Minecraft.OrleansInterfaces.IntVec3)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::Minecraft.OrleansInterfaces.IntVec3), stream);
            result.@mVersion = (global::Minecraft.OrleansInterfaces.MinecraftVersion)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::Minecraft.OrleansInterfaces.MinecraftVersion), stream);
            return (global::Minecraft.OrleansInterfaces.BlockInfo)result;
        }

        public static void Register()
        {
            global::Orleans.Serialization.SerializationManager.@Register(typeof (global::Minecraft.OrleansInterfaces.BlockInfo), DeepCopier, Serializer, Deserializer);
        }

        static OrleansCodeGenMinecraft_OrleansInterfaces_BlockInfoSerializer()
        {
            Register();
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.3.1.0"), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.SerializerAttribute(typeof (global::Minecraft.OrleansInterfaces.ResponseToChunkUpdateMessage)), global::Orleans.CodeGeneration.RegisterSerializerAttribute]
    internal class OrleansCodeGenMinecraft_OrleansInterfaces_ResponseToChunkUpdateMessageSerializer
    {
        private static readonly global::System.Reflection.FieldInfo field2 = typeof (global::Minecraft.OrleansInterfaces.ResponseToChunkUpdateMessage).@GetTypeInfo().@GetField("mAvailableBlocks", (System.@Reflection.@BindingFlags.@Instance | System.@Reflection.@BindingFlags.@NonPublic | System.@Reflection.@BindingFlags.@Public));
        private static readonly global::System.Func<global::Minecraft.OrleansInterfaces.ResponseToChunkUpdateMessage, global::System.Int32> getField2 = (global::System.Func<global::Minecraft.OrleansInterfaces.ResponseToChunkUpdateMessage, global::System.Int32>)global::Orleans.Serialization.SerializationManager.@GetGetter(field2);
        private static readonly global::System.Action<global::Minecraft.OrleansInterfaces.ResponseToChunkUpdateMessage, global::System.Int32> setField2 = (global::System.Action<global::Minecraft.OrleansInterfaces.ResponseToChunkUpdateMessage, global::System.Int32>)global::Orleans.Serialization.SerializationManager.@GetReferenceSetter(field2);
        private static readonly global::System.Reflection.FieldInfo field4 = typeof (global::Minecraft.OrleansInterfaces.ResponseToChunkUpdateMessage).@GetTypeInfo().@GetField("mAvailablePlayers", (System.@Reflection.@BindingFlags.@Instance | System.@Reflection.@BindingFlags.@NonPublic | System.@Reflection.@BindingFlags.@Public));
        private static readonly global::System.Func<global::Minecraft.OrleansInterfaces.ResponseToChunkUpdateMessage, global::System.Int32> getField4 = (global::System.Func<global::Minecraft.OrleansInterfaces.ResponseToChunkUpdateMessage, global::System.Int32>)global::Orleans.Serialization.SerializationManager.@GetGetter(field4);
        private static readonly global::System.Action<global::Minecraft.OrleansInterfaces.ResponseToChunkUpdateMessage, global::System.Int32> setField4 = (global::System.Action<global::Minecraft.OrleansInterfaces.ResponseToChunkUpdateMessage, global::System.Int32>)global::Orleans.Serialization.SerializationManager.@GetReferenceSetter(field4);
        private static readonly global::System.Reflection.FieldInfo field0 = typeof (global::Minecraft.OrleansInterfaces.ResponseToChunkUpdateMessage).@GetTypeInfo().@GetField("mChunkID", (System.@Reflection.@BindingFlags.@Instance | System.@Reflection.@BindingFlags.@NonPublic | System.@Reflection.@BindingFlags.@Public));
        private static readonly global::System.Func<global::Minecraft.OrleansInterfaces.ResponseToChunkUpdateMessage, global::System.String> getField0 = (global::System.Func<global::Minecraft.OrleansInterfaces.ResponseToChunkUpdateMessage, global::System.String>)global::Orleans.Serialization.SerializationManager.@GetGetter(field0);
        private static readonly global::System.Action<global::Minecraft.OrleansInterfaces.ResponseToChunkUpdateMessage, global::System.String> setField0 = (global::System.Action<global::Minecraft.OrleansInterfaces.ResponseToChunkUpdateMessage, global::System.String>)global::Orleans.Serialization.SerializationManager.@GetReferenceSetter(field0);
        private static readonly global::System.Reflection.FieldInfo field1 = typeof (global::Minecraft.OrleansInterfaces.ResponseToChunkUpdateMessage).@GetTypeInfo().@GetField("mLastBlockVersion", (System.@Reflection.@BindingFlags.@Instance | System.@Reflection.@BindingFlags.@NonPublic | System.@Reflection.@BindingFlags.@Public));
        private static readonly global::System.Func<global::Minecraft.OrleansInterfaces.ResponseToChunkUpdateMessage, global::Minecraft.OrleansInterfaces.MinecraftVersion> getField1 = (global::System.Func<global::Minecraft.OrleansInterfaces.ResponseToChunkUpdateMessage, global::Minecraft.OrleansInterfaces.MinecraftVersion>)global::Orleans.Serialization.SerializationManager.@GetGetter(field1);
        private static readonly global::System.Action<global::Minecraft.OrleansInterfaces.ResponseToChunkUpdateMessage, global::Minecraft.OrleansInterfaces.MinecraftVersion> setField1 = (global::System.Action<global::Minecraft.OrleansInterfaces.ResponseToChunkUpdateMessage, global::Minecraft.OrleansInterfaces.MinecraftVersion>)global::Orleans.Serialization.SerializationManager.@GetReferenceSetter(field1);
        private static readonly global::System.Reflection.FieldInfo field3 = typeof (global::Minecraft.OrleansInterfaces.ResponseToChunkUpdateMessage).@GetTypeInfo().@GetField("mLastPlayersVersion", (System.@Reflection.@BindingFlags.@Instance | System.@Reflection.@BindingFlags.@NonPublic | System.@Reflection.@BindingFlags.@Public));
        private static readonly global::System.Func<global::Minecraft.OrleansInterfaces.ResponseToChunkUpdateMessage, global::Minecraft.OrleansInterfaces.MinecraftVersion> getField3 = (global::System.Func<global::Minecraft.OrleansInterfaces.ResponseToChunkUpdateMessage, global::Minecraft.OrleansInterfaces.MinecraftVersion>)global::Orleans.Serialization.SerializationManager.@GetGetter(field3);
        private static readonly global::System.Action<global::Minecraft.OrleansInterfaces.ResponseToChunkUpdateMessage, global::Minecraft.OrleansInterfaces.MinecraftVersion> setField3 = (global::System.Action<global::Minecraft.OrleansInterfaces.ResponseToChunkUpdateMessage, global::Minecraft.OrleansInterfaces.MinecraftVersion>)global::Orleans.Serialization.SerializationManager.@GetReferenceSetter(field3);
        [global::Orleans.CodeGeneration.CopierMethodAttribute]
        public static global::System.Object DeepCopier(global::System.Object original)
        {
            global::Minecraft.OrleansInterfaces.ResponseToChunkUpdateMessage input = ((global::Minecraft.OrleansInterfaces.ResponseToChunkUpdateMessage)original);
            global::Minecraft.OrleansInterfaces.ResponseToChunkUpdateMessage result = new global::Minecraft.OrleansInterfaces.ResponseToChunkUpdateMessage();
            global::Orleans.@Serialization.@SerializationContext.@Current.@RecordObject(original, result);
            setField2(result, getField2(input));
            setField4(result, getField4(input));
            setField0(result, getField0(input));
            setField1(result, (global::Minecraft.OrleansInterfaces.MinecraftVersion)global::Orleans.Serialization.SerializationManager.@DeepCopyInner(getField1(input)));
            setField3(result, (global::Minecraft.OrleansInterfaces.MinecraftVersion)global::Orleans.Serialization.SerializationManager.@DeepCopyInner(getField3(input)));
            return result;
        }

        [global::Orleans.CodeGeneration.SerializerMethodAttribute]
        public static void Serializer(global::System.Object untypedInput, global::Orleans.Serialization.BinaryTokenStreamWriter stream, global::System.Type expected)
        {
            global::Minecraft.OrleansInterfaces.ResponseToChunkUpdateMessage input = (global::Minecraft.OrleansInterfaces.ResponseToChunkUpdateMessage)untypedInput;
            global::Orleans.Serialization.SerializationManager.@SerializeInner(getField2(input), stream, typeof (global::System.Int32));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(getField4(input), stream, typeof (global::System.Int32));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(getField0(input), stream, typeof (global::System.String));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(getField1(input), stream, typeof (global::Minecraft.OrleansInterfaces.MinecraftVersion));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(getField3(input), stream, typeof (global::Minecraft.OrleansInterfaces.MinecraftVersion));
        }

        [global::Orleans.CodeGeneration.DeserializerMethodAttribute]
        public static global::System.Object Deserializer(global::System.Type expected, global::Orleans.Serialization.BinaryTokenStreamReader stream)
        {
            global::Minecraft.OrleansInterfaces.ResponseToChunkUpdateMessage result = new global::Minecraft.OrleansInterfaces.ResponseToChunkUpdateMessage();
            global::Orleans.@Serialization.@DeserializationContext.@Current.@RecordObject(result);
            setField2(result, (global::System.Int32)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.Int32), stream));
            setField4(result, (global::System.Int32)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.Int32), stream));
            setField0(result, (global::System.String)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.String), stream));
            setField1(result, (global::Minecraft.OrleansInterfaces.MinecraftVersion)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::Minecraft.OrleansInterfaces.MinecraftVersion), stream));
            setField3(result, (global::Minecraft.OrleansInterfaces.MinecraftVersion)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::Minecraft.OrleansInterfaces.MinecraftVersion), stream));
            return (global::Minecraft.OrleansInterfaces.ResponseToChunkUpdateMessage)result;
        }

        public static void Register()
        {
            global::Orleans.Serialization.SerializationManager.@Register(typeof (global::Minecraft.OrleansInterfaces.ResponseToChunkUpdateMessage), DeepCopier, Serializer, Deserializer);
        }

        static OrleansCodeGenMinecraft_OrleansInterfaces_ResponseToChunkUpdateMessageSerializer()
        {
            Register();
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.3.1.0"), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.SerializerAttribute(typeof (global::Minecraft.OrleansInterfaces.MinecraftVersion)), global::Orleans.CodeGeneration.RegisterSerializerAttribute]
    internal class OrleansCodeGenMinecraft_OrleansInterfaces_MinecraftVersionSerializer
    {
        private static readonly global::System.Reflection.FieldInfo field0 = typeof (global::Minecraft.OrleansInterfaces.MinecraftVersion).@GetTypeInfo().@GetField("mID", (System.@Reflection.@BindingFlags.@Instance | System.@Reflection.@BindingFlags.@NonPublic | System.@Reflection.@BindingFlags.@Public));
        private static readonly global::System.Func<global::Minecraft.OrleansInterfaces.MinecraftVersion, global::System.DateTime> getField0 = (global::System.Func<global::Minecraft.OrleansInterfaces.MinecraftVersion, global::System.DateTime>)global::Orleans.Serialization.SerializationManager.@GetGetter(field0);
        private static readonly global::System.Action<global::Minecraft.OrleansInterfaces.MinecraftVersion, global::System.DateTime> setField0 = (global::System.Action<global::Minecraft.OrleansInterfaces.MinecraftVersion, global::System.DateTime>)global::Orleans.Serialization.SerializationManager.@GetReferenceSetter(field0);
        [global::Orleans.CodeGeneration.CopierMethodAttribute]
        public static global::System.Object DeepCopier(global::System.Object original)
        {
            global::Minecraft.OrleansInterfaces.MinecraftVersion input = ((global::Minecraft.OrleansInterfaces.MinecraftVersion)original);
            global::Minecraft.OrleansInterfaces.MinecraftVersion result = new global::Minecraft.OrleansInterfaces.MinecraftVersion();
            global::Orleans.@Serialization.@SerializationContext.@Current.@RecordObject(original, result);
            setField0(result, getField0(input));
            return result;
        }

        [global::Orleans.CodeGeneration.SerializerMethodAttribute]
        public static void Serializer(global::System.Object untypedInput, global::Orleans.Serialization.BinaryTokenStreamWriter stream, global::System.Type expected)
        {
            global::Minecraft.OrleansInterfaces.MinecraftVersion input = (global::Minecraft.OrleansInterfaces.MinecraftVersion)untypedInput;
            global::Orleans.Serialization.SerializationManager.@SerializeInner(getField0(input), stream, typeof (global::System.DateTime));
        }

        [global::Orleans.CodeGeneration.DeserializerMethodAttribute]
        public static global::System.Object Deserializer(global::System.Type expected, global::Orleans.Serialization.BinaryTokenStreamReader stream)
        {
            global::Minecraft.OrleansInterfaces.MinecraftVersion result = new global::Minecraft.OrleansInterfaces.MinecraftVersion();
            global::Orleans.@Serialization.@DeserializationContext.@Current.@RecordObject(result);
            setField0(result, (global::System.DateTime)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.DateTime), stream));
            return (global::Minecraft.OrleansInterfaces.MinecraftVersion)result;
        }

        public static void Register()
        {
            global::Orleans.Serialization.SerializationManager.@Register(typeof (global::Minecraft.OrleansInterfaces.MinecraftVersion), DeepCopier, Serializer, Deserializer);
        }

        static OrleansCodeGenMinecraft_OrleansInterfaces_MinecraftVersionSerializer()
        {
            Register();
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.3.1.0"), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.SerializerAttribute(typeof (global::Minecraft.OrleansInterfaces.PlayerInfo)), global::Orleans.CodeGeneration.RegisterSerializerAttribute]
    internal class OrleansCodeGenMinecraft_OrleansInterfaces_PlayerInfoSerializer
    {
        [global::Orleans.CodeGeneration.CopierMethodAttribute]
        public static global::System.Object DeepCopier(global::System.Object original)
        {
            global::Minecraft.OrleansInterfaces.PlayerInfo input = ((global::Minecraft.OrleansInterfaces.PlayerInfo)original);
            global::Minecraft.OrleansInterfaces.PlayerInfo result = new global::Minecraft.OrleansInterfaces.PlayerInfo();
            global::Orleans.@Serialization.@SerializationContext.@Current.@RecordObject(original, result);
            result.@mCreatorID = input.@mCreatorID;
            result.@mID = input.@mID;
            result.@mLastTouchedID = input.@mLastTouchedID;
            result.@mPosition = (global::Minecraft.OrleansInterfaces.Vec3)global::Orleans.Serialization.SerializationManager.@DeepCopyInner(input.@mPosition);
            result.@mVersion = (global::Minecraft.OrleansInterfaces.MinecraftVersion)global::Orleans.Serialization.SerializationManager.@DeepCopyInner(input.@mVersion);
            return result;
        }

        [global::Orleans.CodeGeneration.SerializerMethodAttribute]
        public static void Serializer(global::System.Object untypedInput, global::Orleans.Serialization.BinaryTokenStreamWriter stream, global::System.Type expected)
        {
            global::Minecraft.OrleansInterfaces.PlayerInfo input = (global::Minecraft.OrleansInterfaces.PlayerInfo)untypedInput;
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@mCreatorID, stream, typeof (global::System.String));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@mID, stream, typeof (global::System.String));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@mLastTouchedID, stream, typeof (global::System.String));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@mPosition, stream, typeof (global::Minecraft.OrleansInterfaces.Vec3));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(input.@mVersion, stream, typeof (global::Minecraft.OrleansInterfaces.MinecraftVersion));
        }

        [global::Orleans.CodeGeneration.DeserializerMethodAttribute]
        public static global::System.Object Deserializer(global::System.Type expected, global::Orleans.Serialization.BinaryTokenStreamReader stream)
        {
            global::Minecraft.OrleansInterfaces.PlayerInfo result = new global::Minecraft.OrleansInterfaces.PlayerInfo();
            global::Orleans.@Serialization.@DeserializationContext.@Current.@RecordObject(result);
            result.@mCreatorID = (global::System.String)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.String), stream);
            result.@mID = (global::System.String)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.String), stream);
            result.@mLastTouchedID = (global::System.String)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::System.String), stream);
            result.@mPosition = (global::Minecraft.OrleansInterfaces.Vec3)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::Minecraft.OrleansInterfaces.Vec3), stream);
            result.@mVersion = (global::Minecraft.OrleansInterfaces.MinecraftVersion)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::Minecraft.OrleansInterfaces.MinecraftVersion), stream);
            return (global::Minecraft.OrleansInterfaces.PlayerInfo)result;
        }

        public static void Register()
        {
            global::Orleans.Serialization.SerializationManager.@Register(typeof (global::Minecraft.OrleansInterfaces.PlayerInfo), DeepCopier, Serializer, Deserializer);
        }

        static OrleansCodeGenMinecraft_OrleansInterfaces_PlayerInfoSerializer()
        {
            Register();
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.3.1.0"), global::System.SerializableAttribute, global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.GrainReferenceAttribute(typeof (global::Minecraft.OrleansInterfaces.Grains.IPlayerObserver))]
    internal class OrleansCodeGenPlayerObserverReference : global::Orleans.Runtime.GrainReference, global::Minecraft.OrleansInterfaces.Grains.IPlayerObserver
    {
        protected @OrleansCodeGenPlayerObserverReference(global::Orleans.Runtime.GrainReference @other): base (@other)
        {
        }

        protected @OrleansCodeGenPlayerObserverReference(global::System.Runtime.Serialization.SerializationInfo @info, global::System.Runtime.Serialization.StreamingContext @context): base (@info, @context)
        {
        }

        protected override global::System.Int32 InterfaceId
        {
            get
            {
                return 1827708427;
            }
        }

        public override global::System.String InterfaceName
        {
            get
            {
                return "global::Minecraft.OrleansInterfaces.Grains.IPlayerObserver";
            }
        }

        public override global::System.Boolean @IsCompatible(global::System.Int32 @interfaceId)
        {
            return @interfaceId == 1827708427;
        }

        protected override global::System.String @GetMethodName(global::System.Int32 @interfaceId, global::System.Int32 @methodId)
        {
            switch (@interfaceId)
            {
                case 1827708427:
                    switch (@methodId)
                    {
                        case 1532254592:
                            return "Update";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + 1827708427 + ",methodId=" + @methodId);
                    }

                default:
                    throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
            }
        }

        public void @Update(global::Minecraft.OrleansInterfaces.BlockInfo[] @blocks, global::System.Int32 @nBlocks, global::Minecraft.OrleansInterfaces.PlayerInfo[] @players, global::System.Int32 @nPlayers)
        {
            base.@InvokeOneWayMethod(1532254592, new global::System.Object[]{@blocks, @nBlocks, @players, @nPlayers});
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.3.1.0"), global::Orleans.CodeGeneration.MethodInvokerAttribute("global::Minecraft.OrleansInterfaces.Grains.IPlayerObserver", 1827708427, typeof (global::Minecraft.OrleansInterfaces.Grains.IPlayerObserver)), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    internal class OrleansCodeGenPlayerObserverMethodInvoker : global::Orleans.CodeGeneration.IGrainMethodInvoker
    {
        public global::System.Threading.Tasks.Task<global::System.Object> @Invoke(global::Orleans.Runtime.IAddressable @grain, global::Orleans.CodeGeneration.InvokeMethodRequest @request)
        {
            global::System.Int32 interfaceId = @request.@InterfaceId;
            global::System.Int32 methodId = @request.@MethodId;
            global::System.Object[] arguments = @request.@Arguments;
            if (@grain == null)
                throw new global::System.ArgumentNullException("grain");
            switch (interfaceId)
            {
                case 1827708427:
                    switch (methodId)
                    {
                        case 1532254592:
                            ((global::Minecraft.OrleansInterfaces.Grains.IPlayerObserver)@grain).@Update((global::Minecraft.OrleansInterfaces.BlockInfo[])arguments[0], (global::System.Int32)arguments[1], (global::Minecraft.OrleansInterfaces.PlayerInfo[])arguments[2], (global::System.Int32)arguments[3]);
                            return global::Orleans.Async.TaskUtility.@Completed();
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + 1827708427 + ",methodId=" + methodId);
                    }

                default:
                    throw new global::System.NotImplementedException("interfaceId=" + interfaceId);
            }
        }

        public global::System.Int32 InterfaceId
        {
            get
            {
                return 1827708427;
            }
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.3.1.0"), global::System.SerializableAttribute, global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.GrainReferenceAttribute(typeof (global::Minecraft.OrleansInterfaces.Grains.IWorldGrain))]
    internal class OrleansCodeGenWorldGrainReference : global::Orleans.Runtime.GrainReference, global::Minecraft.OrleansInterfaces.Grains.IWorldGrain
    {
        protected @OrleansCodeGenWorldGrainReference(global::Orleans.Runtime.GrainReference @other): base (@other)
        {
        }

        protected @OrleansCodeGenWorldGrainReference(global::System.Runtime.Serialization.SerializationInfo @info, global::System.Runtime.Serialization.StreamingContext @context): base (@info, @context)
        {
        }

        protected override global::System.Int32 InterfaceId
        {
            get
            {
                return 955027558;
            }
        }

        public override global::System.String InterfaceName
        {
            get
            {
                return "global::Minecraft.OrleansInterfaces.Grains.IWorldGrain";
            }
        }

        public override global::System.Boolean @IsCompatible(global::System.Int32 @interfaceId)
        {
            return @interfaceId == 955027558 || @interfaceId == -1277021679;
        }

        protected override global::System.String @GetMethodName(global::System.Int32 @interfaceId, global::System.Int32 @methodId)
        {
            switch (@interfaceId)
            {
                case 955027558:
                    switch (@methodId)
                    {
                        case -1319440984:
                            return "Create";
                        case -1168913303:
                            return "GetInfo";
                        case 1920068963:
                            return "GetNumberOffChunks";
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + 955027558 + ",methodId=" + @methodId);
                    }

                case -1277021679:
                    switch (@methodId)
                    {
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + -1277021679 + ",methodId=" + @methodId);
                    }

                default:
                    throw new global::System.NotImplementedException("interfaceId=" + @interfaceId);
            }
        }

        public global::System.Threading.Tasks.Task<global::Minecraft.OrleansInterfaces.FeedbackMessage> @Create(global::System.String @sessionID, global::Minecraft.OrleansInterfaces.IntVec3 @beginPoint, global::Minecraft.OrleansInterfaces.IntVec3 @endPoint, global::Minecraft.OrleansInterfaces.IntVec3 @chunkSize, global::Minecraft.OrleansInterfaces.IntVec3 @visibilityChunkCount)
        {
            return base.@InvokeMethodAsync<global::Minecraft.OrleansInterfaces.FeedbackMessage>(-1319440984, new global::System.Object[]{@sessionID, @beginPoint, @endPoint, @chunkSize, @visibilityChunkCount});
        }

        public global::System.Threading.Tasks.Task<global::Minecraft.OrleansInterfaces.Grains.WorldInfoResult> @GetInfo()
        {
            return base.@InvokeMethodAsync<global::Minecraft.OrleansInterfaces.Grains.WorldInfoResult>(-1168913303, null);
        }

        public global::System.Threading.Tasks.Task<global::System.Int32> @GetNumberOffChunks()
        {
            return base.@InvokeMethodAsync<global::System.Int32>(1920068963, null);
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.3.1.0"), global::Orleans.CodeGeneration.MethodInvokerAttribute("global::Minecraft.OrleansInterfaces.Grains.IWorldGrain", 955027558, typeof (global::Minecraft.OrleansInterfaces.Grains.IWorldGrain)), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute]
    internal class OrleansCodeGenWorldGrainMethodInvoker : global::Orleans.CodeGeneration.IGrainMethodInvoker
    {
        public global::System.Threading.Tasks.Task<global::System.Object> @Invoke(global::Orleans.Runtime.IAddressable @grain, global::Orleans.CodeGeneration.InvokeMethodRequest @request)
        {
            global::System.Int32 interfaceId = @request.@InterfaceId;
            global::System.Int32 methodId = @request.@MethodId;
            global::System.Object[] arguments = @request.@Arguments;
            if (@grain == null)
                throw new global::System.ArgumentNullException("grain");
            switch (interfaceId)
            {
                case 955027558:
                    switch (methodId)
                    {
                        case -1319440984:
                            return ((global::Minecraft.OrleansInterfaces.Grains.IWorldGrain)@grain).@Create((global::System.String)arguments[0], (global::Minecraft.OrleansInterfaces.IntVec3)arguments[1], (global::Minecraft.OrleansInterfaces.IntVec3)arguments[2], (global::Minecraft.OrleansInterfaces.IntVec3)arguments[3], (global::Minecraft.OrleansInterfaces.IntVec3)arguments[4]).@Box();
                        case -1168913303:
                            return ((global::Minecraft.OrleansInterfaces.Grains.IWorldGrain)@grain).@GetInfo().@Box();
                        case 1920068963:
                            return ((global::Minecraft.OrleansInterfaces.Grains.IWorldGrain)@grain).@GetNumberOffChunks().@Box();
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + 955027558 + ",methodId=" + methodId);
                    }

                case -1277021679:
                    switch (methodId)
                    {
                        default:
                            throw new global::System.NotImplementedException("interfaceId=" + -1277021679 + ",methodId=" + methodId);
                    }

                default:
                    throw new global::System.NotImplementedException("interfaceId=" + interfaceId);
            }
        }

        public global::System.Int32 InterfaceId
        {
            get
            {
                return 955027558;
            }
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Orleans-CodeGenerator", "1.3.1.0"), global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute, global::Orleans.CodeGeneration.SerializerAttribute(typeof (global::Minecraft.OrleansInterfaces.Grains.WorldInfoResult)), global::Orleans.CodeGeneration.RegisterSerializerAttribute]
    internal class OrleansCodeGenMinecraft_OrleansInterfaces_Grains_WorldInfoResultSerializer
    {
        private static readonly global::System.Reflection.FieldInfo field0 = typeof (global::Minecraft.OrleansInterfaces.Grains.WorldInfoResult).@GetTypeInfo().@GetField("chunkSize", (System.@Reflection.@BindingFlags.@Instance | System.@Reflection.@BindingFlags.@NonPublic | System.@Reflection.@BindingFlags.@Public));
        private static readonly global::System.Func<global::Minecraft.OrleansInterfaces.Grains.WorldInfoResult, global::Minecraft.OrleansInterfaces.IntVec3> getField0 = (global::System.Func<global::Minecraft.OrleansInterfaces.Grains.WorldInfoResult, global::Minecraft.OrleansInterfaces.IntVec3>)global::Orleans.Serialization.SerializationManager.@GetGetter(field0);
        private static readonly global::System.Action<global::Minecraft.OrleansInterfaces.Grains.WorldInfoResult, global::Minecraft.OrleansInterfaces.IntVec3> setField0 = (global::System.Action<global::Minecraft.OrleansInterfaces.Grains.WorldInfoResult, global::Minecraft.OrleansInterfaces.IntVec3>)global::Orleans.Serialization.SerializationManager.@GetReferenceSetter(field0);
        private static readonly global::System.Reflection.FieldInfo field2 = typeof (global::Minecraft.OrleansInterfaces.Grains.WorldInfoResult).@GetTypeInfo().@GetField("maxLocation", (System.@Reflection.@BindingFlags.@Instance | System.@Reflection.@BindingFlags.@NonPublic | System.@Reflection.@BindingFlags.@Public));
        private static readonly global::System.Func<global::Minecraft.OrleansInterfaces.Grains.WorldInfoResult, global::Minecraft.OrleansInterfaces.IntVec3> getField2 = (global::System.Func<global::Minecraft.OrleansInterfaces.Grains.WorldInfoResult, global::Minecraft.OrleansInterfaces.IntVec3>)global::Orleans.Serialization.SerializationManager.@GetGetter(field2);
        private static readonly global::System.Action<global::Minecraft.OrleansInterfaces.Grains.WorldInfoResult, global::Minecraft.OrleansInterfaces.IntVec3> setField2 = (global::System.Action<global::Minecraft.OrleansInterfaces.Grains.WorldInfoResult, global::Minecraft.OrleansInterfaces.IntVec3>)global::Orleans.Serialization.SerializationManager.@GetReferenceSetter(field2);
        private static readonly global::System.Reflection.FieldInfo field1 = typeof (global::Minecraft.OrleansInterfaces.Grains.WorldInfoResult).@GetTypeInfo().@GetField("minLocation", (System.@Reflection.@BindingFlags.@Instance | System.@Reflection.@BindingFlags.@NonPublic | System.@Reflection.@BindingFlags.@Public));
        private static readonly global::System.Func<global::Minecraft.OrleansInterfaces.Grains.WorldInfoResult, global::Minecraft.OrleansInterfaces.IntVec3> getField1 = (global::System.Func<global::Minecraft.OrleansInterfaces.Grains.WorldInfoResult, global::Minecraft.OrleansInterfaces.IntVec3>)global::Orleans.Serialization.SerializationManager.@GetGetter(field1);
        private static readonly global::System.Action<global::Minecraft.OrleansInterfaces.Grains.WorldInfoResult, global::Minecraft.OrleansInterfaces.IntVec3> setField1 = (global::System.Action<global::Minecraft.OrleansInterfaces.Grains.WorldInfoResult, global::Minecraft.OrleansInterfaces.IntVec3>)global::Orleans.Serialization.SerializationManager.@GetReferenceSetter(field1);
        [global::Orleans.CodeGeneration.CopierMethodAttribute]
        public static global::System.Object DeepCopier(global::System.Object original)
        {
            global::Minecraft.OrleansInterfaces.Grains.WorldInfoResult input = ((global::Minecraft.OrleansInterfaces.Grains.WorldInfoResult)original);
            global::Minecraft.OrleansInterfaces.Grains.WorldInfoResult result = new global::Minecraft.OrleansInterfaces.Grains.WorldInfoResult();
            global::Orleans.@Serialization.@SerializationContext.@Current.@RecordObject(original, result);
            setField0(result, (global::Minecraft.OrleansInterfaces.IntVec3)global::Orleans.Serialization.SerializationManager.@DeepCopyInner(getField0(input)));
            setField2(result, (global::Minecraft.OrleansInterfaces.IntVec3)global::Orleans.Serialization.SerializationManager.@DeepCopyInner(getField2(input)));
            setField1(result, (global::Minecraft.OrleansInterfaces.IntVec3)global::Orleans.Serialization.SerializationManager.@DeepCopyInner(getField1(input)));
            return result;
        }

        [global::Orleans.CodeGeneration.SerializerMethodAttribute]
        public static void Serializer(global::System.Object untypedInput, global::Orleans.Serialization.BinaryTokenStreamWriter stream, global::System.Type expected)
        {
            global::Minecraft.OrleansInterfaces.Grains.WorldInfoResult input = (global::Minecraft.OrleansInterfaces.Grains.WorldInfoResult)untypedInput;
            global::Orleans.Serialization.SerializationManager.@SerializeInner(getField0(input), stream, typeof (global::Minecraft.OrleansInterfaces.IntVec3));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(getField2(input), stream, typeof (global::Minecraft.OrleansInterfaces.IntVec3));
            global::Orleans.Serialization.SerializationManager.@SerializeInner(getField1(input), stream, typeof (global::Minecraft.OrleansInterfaces.IntVec3));
        }

        [global::Orleans.CodeGeneration.DeserializerMethodAttribute]
        public static global::System.Object Deserializer(global::System.Type expected, global::Orleans.Serialization.BinaryTokenStreamReader stream)
        {
            global::Minecraft.OrleansInterfaces.Grains.WorldInfoResult result = new global::Minecraft.OrleansInterfaces.Grains.WorldInfoResult();
            global::Orleans.@Serialization.@DeserializationContext.@Current.@RecordObject(result);
            setField0(result, (global::Minecraft.OrleansInterfaces.IntVec3)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::Minecraft.OrleansInterfaces.IntVec3), stream));
            setField2(result, (global::Minecraft.OrleansInterfaces.IntVec3)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::Minecraft.OrleansInterfaces.IntVec3), stream));
            setField1(result, (global::Minecraft.OrleansInterfaces.IntVec3)global::Orleans.Serialization.SerializationManager.@DeserializeInner(typeof (global::Minecraft.OrleansInterfaces.IntVec3), stream));
            return (global::Minecraft.OrleansInterfaces.Grains.WorldInfoResult)result;
        }

        public static void Register()
        {
            global::Orleans.Serialization.SerializationManager.@Register(typeof (global::Minecraft.OrleansInterfaces.Grains.WorldInfoResult), DeepCopier, Serializer, Deserializer);
        }

        static OrleansCodeGenMinecraft_OrleansInterfaces_Grains_WorldInfoResultSerializer()
        {
            Register();
        }
    }
}
#pragma warning restore 162
#pragma warning restore 219
#pragma warning restore 414
#pragma warning restore 649
#pragma warning restore 693
#pragma warning restore 1591
#pragma warning restore 1998
#endif
