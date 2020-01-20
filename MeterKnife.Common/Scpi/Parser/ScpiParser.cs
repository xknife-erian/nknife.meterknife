using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace NKnife.MeterKnife.Common.Scpi.Parser
{
/**
 * The <code>SCPIParser</code> is a general purpose command parser for
 * SCPI-style commands.
 *
 * Refer to
 * <a href="http://en.wikipedia.org/wiki/Standard_Commands_for_Programmable_Instruments">Standard
 * Commands for Programmable Instruments</a> on Wikipedia for more information
 * about SCPI.
 *
 * <h2>Usage</h2>
 *
 * <p>
 * To the {@link SCPIParser} class can be used in two ways. First, you can use
 * the class as-is by calling {@link addHandler} to add handlers to
 * {@link SCPIParser} objects.</p>
 *
 * <p>
 * The preferred usage is to extend the {@link SCPIParser} class and register
 * handlers in the constructor of the extending class. New handlers are
 * registered by calling the {@link #addHandler addHandler} method and
 * specifying the full, unabbreviated SCPI command path and an
 * {@link SCPICommandHandler}. Command handlers must implement the
 * {@link SCPICommandHandler} functional interface. For example,</p>
 *
 * <h3>Example Usage</h3>
 * <pre>
 * {@code class SimpleSCPIParser extends SCPIParser {
 *    public SimpleSCPIParser() {
 *      addHandler("*IDN?", this::IDN);
 *    }
 *
 *    string IDN(string[] args) {
 *          return "Simple SCPI Parser";
 *    }
 * }
 *
 * SimpleSCPIParser myParser = new SimpleSCPIParser();
 * for (string result : myParser.accept("*IDN?")) {
 *   System.out.println(result);
 * }}
 * </pre>
 *
 * <p>
 * The parser will correctly interpret chained commands of the form
 * <code>*IDN?;MEAS:VOLT:DC?;AC?</code>. In this case, three commands would be
 * processed, where the readonly command <code>MEAS:VOLT:AC?</code> would be
 * correctly interpreted.</p>
 *
 * <h2>Performance Considerations</h2>
 * <p>
 * By default, the SCPIParser caches
 * {@link #getCacheSizeLimit getCacheSizeLimit()} number of queries, greatly
 * speeding execution of <em>repeated</em> queries (default is 20). This
 * optimizes performance when the parser accepts many <em>identical</em>
 * queries. (Only the parsed version of queries is cached. Results are computed
 * for each call to {@link #accept accept(string query)}.)</p>
 *
 * <p>
 * Commands containing argument values are not cached by default. (For example,
 * queries that send data to the parser.) This prevents caching queries that are
 * not expected to be repeated often. However, if only a small number of queries
 * containing identical argument values can be expected, then caching can be
 * enabled by calling
 * {@link #setCacheQueriesWithArguments setCacheQueriesWithArguments(true)}.</p>
 *
 */

    // public class ScpiParser
    // {
    //
    //     private readonly Dictionary<ScpiPath, IScpiCommandHandler> _Handlers = new Dictionary<ScpiPath, IScpiCommandHandler>();
    //     private readonly Dictionary<string, string> _ShortToLongCmd = new Dictionary<string, string>();
    //     private readonly Dictionary<string, List<ScpiCommandCaller>> _AcceptCache = new Dictionary<string, List<ScpiCommandCaller>>();
    //     private readonly Dictionary<string, LongAdder> _AcceptCacheKeyFrequency = new Dictionary<string, LongAdder>();
    //     private static readonly Pattern _tokenPatterns;
    //     private static readonly Pattern _upperMatch;
    //     private static readonly IScpiCommandHandler _nullCmdHandler;
    //     private static int MAX_CACHE_SIZE = 20;
    //     private static bool CACHE_QUERIES_WITH_ARGUMENTS = false;
    //
    //     static ScpiParser()
    //     {
    //         _tokenPatterns = buildLexer();
    //         _upperMatch = Pattern.compile("[A-Z_*]+");
    //         _nullCmdHandler = (string[] args) ->
    //         null;
    //     }
    //
    //     public ScpiParser()
    //     {
    //     }
    //
    //     /**
    //  * Adds a <code>SCPICommandHandler</code> for a specified SCPI path.
    //  *
    //  * @param path an absolute SCPI path
    //  * @param handler the method to associate with the path
    //  */
    //
    //     public void addHandler(string path, IScpiCommandHandler handler)
    //     {
    //         ScpiPath scpipath = new ScpiPath(path);
    //         IEnumerable<string> elements = scpipath.Iterator();
    //
    //         while (element.hasNext())
    //         {
    //             string element = getNonQueryPathElement(elements.next());
    //             Matcher matcher = _upperMatch.matcher(element);
    //             if (matcher.find())
    //             {
    //                 _ShortToLongCmd.put(matcher.group(), element);
    //             }
    //         }
    //         _Handlers.put(scpipath, handler);
    //     }
    //
    //     private bool isQuery(string input)
    //     {
    //         char lastChar = input[input.Length - 1];
    //         return (lastChar == '?');
    //     }
    //
    //     private string getNonQueryPathElement(string input)
    //     {
    //         if (!isQuery(input))
    //         {
    //             return input;
    //         }
    //         else
    //         {
    //             return input.Substring(0, input.Length - 1);
    //         }
    //     }
    //
    //     /**
    //  * Accepts query input and returns the results of query processing.
    //  *
    //  * Each element in the query is returned as a string, or null if no result
    //  * was returned by the handler.
    //  *
    //  * @param query a string containing input to the parser
    //  * @return returns an array containing the result of each command contained
    //  * in the query (may contain null)
    //  * @throws com.scpi.parser.SCPIParser.SCPIMissingHandlerException this
    //  * exception may be thrown if the query refers to an unmapped function or
    //  * contains an error. The caller should handle this exception.
    //  */
    //
    //     public string[] accept(string query)
    //     {
    //         List<ScpiCommandCaller> commands;
    //         if (MAX_CACHE_SIZE > 0 && _AcceptCache.ContainsKey(query))
    //         {
    //             commands = _AcceptCache[query];
    //             _AcceptCacheKeyFrequency.computeIfPresent(query, (k, v) ->
    //             {
    //                 v.increment();
    //                 return v;
    //             }
    //         )
    //             ;
    //         }
    //         else
    //         {
    //             List<ScpiToken> tokens = lex(query);
    //             commands = parse(tokens);
    //             bool commandsContainsArgument = false;
    //             if (!CACHE_QUERIES_WITH_ARGUMENTS)
    //             {
    //                 for (ScpiToken token :
    //                 tokens)
    //                 {
    //                     if (token.tokenType == ScpiTokenType.ARGUMENT)
    //                     {
    //                         commandsContainsArgument = true;
    //                         break;
    //                     }
    //                 }
    //             }
    //             if (MAX_CACHE_SIZE > 0 && !commandsContainsArgument)
    //             {
    //                 synchronized(this)
    //                 {
    //                     if (_AcceptCache.size() > MAX_CACHE_SIZE - 1)
    //                     {
    //                         string lowestFreqCmd = null;
    //                         Integer lowestFrequency = Integer.MAX_VALUE;
    //                         for (string key : _AcceptCacheKeyFrequency.keySet())
    //                         {
    //                             Integer count = _AcceptCacheKeyFrequency.get(key).intValue();
    //                             if (count < lowestFrequency)
    //                             {
    //                                 lowestFrequency = count;
    //                                 lowestFreqCmd = key;
    //                             }
    //                         }
    //                         _AcceptCacheKeyFrequency.remove(lowestFreqCmd);
    //                         _AcceptCache.remove(lowestFreqCmd);
    //                     }
    //                     _AcceptCache.put(query, commands);
    //                     _AcceptCacheKeyFrequency.computeIfAbsent(query, k->
    //                     {
    //                         LongAdder longAdder = new LongAdder();
    //                         longAdder.increment();
    //                         return longAdder;
    //                     }
    //                 )
    //                     ;
    //                 }
    //             }
    //         }
    //         string[] results = new string[commands.size()];
    //         int index = 0;
    //         foreach (var command in commands)
    //         {
    //             results[index++] = command.Execute();
    //         }
    //         return results;
    //     }
    //
    //     /* (non-Javadoc)
    //  * This command should not be used in production code.
    //  * It returns a reference to the internal key cache frequency map and
    //  * may be used for testing and optimization purposes.
    //  * 
    //  * It is <b>not safe</b> to modify the contents of this map.
    //  * @return a reference to the internal cache frequency map.
    //  */
    //
    //     private Dictionary<string, LongAdder> getCacheFrequency()
    //     {
    //         return _AcceptCacheKeyFrequency;
    //     }
    //
    //     /**
    //  * If set to true, then queries containing argument values will be cached.
    //  * This can negatively impact performance if many unique queries are sent to
    //  * the parser.
    //  *
    //  * @param newValue desired caching state for queries that contain arguments.
    //  */
    //
    //     public void setCacheQueriesWithArguments(bool newValue)
    //     {
    //         CACHE_QUERIES_WITH_ARGUMENTS = newValue;
    //     }
    //
    //     /**
    //  *
    //  * @return the current state of the caching queries containing arguments.
    //  */
    //
    //     public bool isCacheQueriesWithArguments()
    //     {
    //         return CACHE_QUERIES_WITH_ARGUMENTS;
    //     }
    //
    //     /**
    //  * Sets the desired cache size limit (number of unique queries). Setting the
    //  * size to 0 will effectively disable caching. Negative values are
    //  * interpreted as 0.
    //  *
    //  * @param newSize the desired cache size
    //  */
    //
    //     public void setCacheSizeLimit(int newSize)
    //     {
    //         MAX_CACHE_SIZE = (newSize >= 0) ? newSize : 0;
    //     }
    //
    //     /**
    //  *
    //  * @return current cache size limit
    //  */
    //
    //     public int getCacheSizeLimit()
    //     {
    //         return MAX_CACHE_SIZE;
    //     }
    //
    //     private List<ScpiCommandCaller> parse(List<ScpiToken> tokens)
    //     {
    //         List<ScpiCommandCaller> commands = new List<ScpiCommandCaller>();
    //         ScpiPath activePath = new ScpiPath();
    //         List<string> arguments = new List<string>();
    //         bool inCommand = false;
    //         foreach (var token in tokens)
    //         {
    //             switch (token.tokenType)
    //             {
    //                 case COMMAND:
    //                     // normalize all commands to long-version
    //                     bool isQuery = isQuery(token.data);
    //                     string queryKey = getNonQueryPathElement(token.data);
    //                     string longCmd = _ShortToLongCmd.get(queryKey);
    //                     longCmd = (!isQuery) ? longCmd : longCmd + "?";
    //                     activePath.append((null == longCmd) ? token.data : longCmd);
    //                     inCommand = true;
    //                     break;
    //                 case ARGUMENT:
    //                 case QUOTEDstring:
    //                     arguments.add(token.data);
    //                     break;
    //                 case COLON:
    //                     if (!inCommand)
    //                     {
    //                         activePath.Clear();
    //                     }
    //                     break;
    //                 case SEMICOLON:
    //                     // try to handle the current path
    //                     IScpiCommandHandler activeHandler = _Handlers.get(activePath);
    //                     if (null != activeHandler)
    //                     {
    //                         commands.add(new ScpiCommandCaller(activeHandler,
    //                             arguments.toArray(new string[arguments.size()])));
    //                     }
    //                     else
    //                     {
    //                         commands.add(new ScpiCommandCaller(_nullCmdHandler, new string[] {}));
    //                         throw new ScpiMissingHandlerException(activePath.Tostring());
    //                     }
    //                     arguments.clear();
    //                     inCommand = false;
    //                     activePath.Strip();
    //                     break;
    //                 case NEWLINE:
    //                 case WHITESPACE:
    //                 default:
    //                     break;
    //             }
    //         }
    //         return commands;
    //     }
    //
    //
    //     private List<ScpiToken> lex(string input)
    //     {
    //         ArrayList<ScpiToken> tokens = new ArrayList<>();
    //
    //         // see optimization note for "tokenTypes" in SCPITokenType enum
    //         ScpiTokenType[] tokenTypes = ScpiTokenType.tokenTypes;
    //
    //         Matcher matcher = _tokenPatterns.matcher(input);
    //         ScpiTokenType prevTokenType = ScpiTokenType.WHITESPACE;
    //         while (matcher.find())
    //         {
    //             foreach (var tokenType in ScpiTokenType.Values)
    //             {
    //                 string group = matcher.group(tokenType.name());
    //                 if (group != null)
    //                 {
    //                     switch (tokenType)
    //                     {
    //                         case QUOTEDstring:
    //                             group = group.substring(1, group.length() - 1);
    //                         case COMMAND:
    //                         // fall through
    //                         case ARGUMENT:
    //                             ScpiTokenType typeToAdd = tokenType;
    //                             if (prevTokenType == ScpiTokenType.COMMAND)
    //                             {
    //                                 typeToAdd = ScpiTokenType.ARGUMENT;
    //                             }
    //                             tokens.add(new ScpiToken(typeToAdd, group));
    //                             prevTokenType = ScpiTokenType.COMMAND;
    //                             break;
    //                         case COLON:
    //                         // fall through
    //                         case SEMICOLON:
    //                             if (tokenType != prevTokenType)
    //                             {
    //                                 tokens.add(new ScpiToken(tokenType, null));
    //                                 prevTokenType = tokenType;
    //                             }
    //                             break;
    //                         default:
    //                             break;
    //                     }
    //                     break;
    //                 }
    //             }
    //         }
    //         if (prevTokenType != ScpiTokenType.SEMICOLON)
    //         {
    //             tokens.add(new ScpiToken(ScpiTokenType.SEMICOLON, null));
    //         }
    //         return tokens;
    //     }
    //
    //     private static Pattern buildLexer()
    //     {
    //         StringBuilder tokenPatternsBuffer = new StringBuilder();
    //         foreach (var value in ScpiTokenType.Values)
    //         {
    //             tokenPatternsBuffer.Append(string.Format("|(?<{0}>{1})", tokenType.name(), tokenType.pattern));
    //         }
    //         return Pattern.compile(tokenPatternsBuffer.SubString(1));
    //     }
    //
    // }
}