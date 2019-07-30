import { makeExecutableSchema } from "graphql-tools";
import { ApolloServer } from "apollo-server";
import { merge } from "lodash";
import { GraphQLError, GraphQLFormattedError } from "graphql";
import { importSchema } from "graphql-import";

import { leadResolver } from "resolvers";
import { MyContext } from "context";

const typeDefs = importSchema(
  "./src/generated/graphql/generated-schema.graphql"
);

const schema = makeExecutableSchema({
  typeDefs: [typeDefs],
  resolvers: merge(leadResolver)
});

const server: ApolloServer = new ApolloServer({
  schema,
  formatError(error: GraphQLError): GraphQLFormattedError {
    console.log(error);
    return error;
  },
  context: ({ req }) : MyContext => {
    return {
      authToken: req.headers["authorization"]
    } as MyContext;
  }
});

server.listen().then(({ url }): void => {
  console.log(`Running a GraphQL API server at ${url} `);
  return;
});
