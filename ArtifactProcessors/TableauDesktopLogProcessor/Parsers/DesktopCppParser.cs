﻿using LogParsers.Base;
using LogParsers.Base.Helpers;
using LogParsers.Base.Parsers;
using System.Collections.Generic;

namespace Logshark.ArtifactProcessors.TableauDesktopLogProcessor.Parsers
{
    /// <summary>
    /// Parses Desktop C++ logs to JSON.
    /// </summary>
    public sealed class DesktopCppParser : AbstractJsonParser, IParser
    {
        private static readonly string collectionName = ParserConstants.DesktopCollectionName;
        private static readonly IList<string> indexNames = new List<string> { "k", "pid", "sev" };
        private static readonly CollectionSchema collectionSchema = ParserUtil.CreateCollectionSchema(collectionName, indexNames);

        public override CollectionSchema CollectionSchema
        {
            get
            {
                return collectionSchema;
            }
        }

        public DesktopCppParser()
        {
        }

        public DesktopCppParser(LogFileContext fileContext)
            : base(fileContext)
        {
        }
    }
}