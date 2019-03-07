using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using hello.netcore_22.aws.Models;

namespace hello.netcore_22.aws.Exceptions
{
     public class PostException : Exception
    {
        public PostException()
        {
        }

        public PostException(string message) : base(message)
        {
        }

        public PostException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PostException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    public class PostNotFoundException : PostException
    {
        public PostNotFoundException(Post post)
            : base($"Post {post.Title}, ({post.Id}) of {post.Author} was not found.")
        {
        }

        public PostNotFoundException(string postKey)
            : base($"Post {postKey} of was not found.")
        {
        }
    }
}