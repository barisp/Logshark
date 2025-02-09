﻿using Logshark.Common.Extensions;
using Logshark.RequestModel.Config;
using System;
using System.Collections.Generic;
using System.IO;

namespace Logshark.RequestModel
{
    /// <summary>
    /// Encapsulates state about what Logshark should process.
    /// </summary>
    public class LogsharkRequest
    {
        // The default port that MongoDB runs on if run locally.
        private const int MongoLocalPortDefault = 27017;

        #region Public Properties

        // Runtime configuration options for this request.
        public LogsharkConfiguration Configuration { get; private set; }

        public string CustomId { get; set; }

        // Indicates whether the MongoDB instance generated by this request should be dropped following the run.
        public bool DropMongoDBPostRun { get; set; }

        // Force a logset parse even if there's a matching hash in the DB.
        public bool ForceParse { get; set; }

        // The port that Mongo will run on, if run locally.
        public int LocalMongoPort { get; set; }

        // Used to hold metadata about this request.
        public IDictionary<string, object> Metadata { get; }

        // Used for plumbing custom arguments to plugins.
        public IDictionary<string, object> PluginCustomArguments { get; }

        // List of names of plugins to execute as a part of this request.  Also accepts "all" or "none".
        public ISet<string> PluginsToExecute { get; }

        // The name of the backing database to use.  Defaults to RunID.
        public string PostgresDatabaseName
        {
            get { return !string.IsNullOrWhiteSpace(_postgresDatabaseName) ? _postgresDatabaseName : RunId; }
            set { _postgresDatabaseName = value; }
        }

        private string _postgresDatabaseName;

        // Force processing of the full logset, even if we don't need all of the data.
        public bool ProcessFullLogset { get; set; }

        // The description of the Tableau Server project used for any published workbooks.  If not set, a default will be generated.
        public string ProjectDescription
        {
            get { return !string.IsNullOrWhiteSpace(_projectDescription) ? _projectDescription : null; }
            set { _projectDescription = value; }
        }

        private string _projectDescription;

        // The name of the Tableau Server project used for any published workbooks.  Defaults to RunID.
        public string ProjectName
        {
            get { return !string.IsNullOrWhiteSpace(_projectName) ? _projectName : RunId; }
            set { _projectName = value; }
        }

        private string _projectName;

        // Publish workbooks to Tableau Server at end of run.
        public bool PublishWorkbooks { get; set; }

        // Timestamp of when this request was created.
        public DateTime RequestCreationDate { get; }

        // Unique identifier that represents this run.
        public string RunId { get; set; }

        // Indicates whether a local MongoDB process should be spun up for the lifetime of processing this request.
        public bool StartLocalMongo { get; set; }

        // The source that created this request.
        public string Source { get; set; }

        // The target logset to process.
        public LogsharkRequestTarget Target { get; }

        // Tags that should be applied to any published workbooks.
        public ISet<string> WorkbookTags { get; }

        #endregion Public Properties

        internal LogsharkRequest(string target, LogsharkConfiguration configuration)
        {
            Target = new LogsharkRequestTarget(target);
            Configuration = configuration;
            RequestCreationDate = DateTime.UtcNow;
            RunId = GenerateRunId(Target, RequestCreationDate);
            Source = "Unspecified";

            LocalMongoPort = MongoLocalPortDefault;
            Metadata = new Dictionary<string, object>();
            PluginsToExecute = new HashSet<string>();
            PluginCustomArguments = new Dictionary<string, object>();
            StartLocalMongo = configuration.LocalMongoOptions.AlwaysUseLocalMongo;
            WorkbookTags = new HashSet<string> { "Logshark", Environment.UserName };
        }

        private static string GenerateRunId(LogsharkRequestTarget requestTarget, DateTime requestCreationTime)
        {
            var hostnamePrefix = Environment.MachineName;
            var timeStamp = requestCreationTime.ToString("yyMMddHHmmssff");

            var targetSuffix = requestTarget.Type == LogsetTarget.Hash
                ? requestTarget.Target 
                : Path.GetFileName(requestTarget).RemoveSpecialCharacters();

            return $"{hostnamePrefix}_{timeStamp}_{targetSuffix}"
                .ToLowerInvariant()
                .Left(60);
        }
    }
}