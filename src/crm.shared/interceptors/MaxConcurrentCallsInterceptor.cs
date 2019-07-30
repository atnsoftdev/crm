using System;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Core.Interceptors;

namespace CRM.Shared.Interceptors
{
    public class MaxConcurrentCallsInterceptor :Interceptor
    {
        private static readonly RpcException _maxConcurrentCallsExceededException = new RpcException(new Status(StatusCode.ResourceExhausted, "Maximum number of concurrent calls exceeded."), "Maximum number of concurrent calls exceeded.");
        private SemaphoreSlim _concurrentCalls;

        public MaxConcurrentCallsInterceptor(int maxConcurrentCalls)
        {
            if(maxConcurrentCalls <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maxConcurrentCalls), maxConcurrentCalls, $"{nameof(maxConcurrentCalls)} must be a positive number.");
            }

            _concurrentCalls = new SemaphoreSlim(maxConcurrentCalls, maxConcurrentCalls);
        }

        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request, ServerCallContext context, UnaryServerMethod<TRequest, TResponse> continuation)
        {
            CheckConcurrentLimit();
            try
            {
                return await continuation(request, context);
            }
            finally
            {
                _concurrentCalls.Release();
            }
        }

        public override async Task<TResponse> ClientStreamingServerHandler<TRequest, TResponse>(IAsyncStreamReader<TRequest> requestStream, ServerCallContext context, ClientStreamingServerMethod<TRequest, TResponse> continuation)
        {
            CheckConcurrentLimit();

            try
            {
                return await continuation(requestStream, context);
            }
            finally
            {
                _concurrentCalls.Release();
            }
        }

        public override async Task ServerStreamingServerHandler<TRequest, TResponse>(TRequest request, IServerStreamWriter<TResponse> responseStream, ServerCallContext context, ServerStreamingServerMethod<TRequest, TResponse> continuation)
        {
            CheckConcurrentLimit();

            try
            {
                await continuation(request, responseStream, context);
            }
            finally
            {
                _concurrentCalls.Release();
            }
        }

        public override async Task DuplexStreamingServerHandler<TRequest, TResponse>(IAsyncStreamReader<TRequest> requestStream, IServerStreamWriter<TResponse> responseStream, ServerCallContext context, DuplexStreamingServerMethod<TRequest, TResponse> continuation)
        {
            CheckConcurrentLimit();

            try
            {
                await continuation(requestStream, responseStream, context);
            }
            finally
            {
                _concurrentCalls.Release();
            }
        }

        private void CheckConcurrentLimit()
        {
            if(!_concurrentCalls.Wait(0))
            {
                throw _maxConcurrentCallsExceededException;
            }
        }
    }
}
