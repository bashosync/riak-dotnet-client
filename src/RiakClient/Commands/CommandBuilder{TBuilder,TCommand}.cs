namespace RiakClient.Commands
{
    using System;

    /// <summary>
    /// Base class for all Riak command builders.
    /// </summary>
    /// <typeparam name="TBuilder">The type of the builder. Allows chaining.</typeparam>
    /// <typeparam name="TCommand">The type of the command.</typeparam>
    public abstract class CommandBuilder<TBuilder, TCommand> : IRCommandBuilder
        where TCommand : IRCommand
        where TBuilder : CommandBuilder<TBuilder, TCommand>
    {
        protected string bucketType;
        protected string bucket;
        protected string key;
        protected TimeSpan timeout = Riak.Constants.DefaultCommandTimeout;

        public CommandBuilder()
        {
        }

        public CommandBuilder(CommandBuilder<TBuilder, TCommand> source)
        {
            this.bucketType = source.bucketType;
            this.bucket = source.bucket;
            this.key = source.key;
            this.timeout = source.timeout;
        }

        public abstract IRCommand Build();

        public TBuilder WithBucketType(string bucketType)
        {
            if (string.IsNullOrWhiteSpace(bucketType))
            {
                throw new ArgumentNullException("bucketType", "bucketType may not be null, empty or whitespace");
            }

            this.bucketType = bucketType;
            return (TBuilder)this;
        }

        public TBuilder WithBucket(string bucket)
        {
            if (string.IsNullOrWhiteSpace(bucket))
            {
                throw new ArgumentNullException("bucket", "bucket may not be null, empty or whitespace");
            }

            this.bucket = bucket;
            return (TBuilder)this;
        }

        public TBuilder WithKey(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException("key", "key may not be null, empty or whitespace");
            }

            this.key = key;
            return (TBuilder)this;
        }

        public TBuilder WithTimeout(TimeSpan timeout)
        {
            if (timeout == default(TimeSpan))
            {
                this.timeout = Riak.Constants.DefaultCommandTimeout;
            }
            else
            {
                this.timeout = timeout;
            }

            return (TBuilder)this;
        }
    }
}