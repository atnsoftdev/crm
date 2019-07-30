import _ from "lodash";

import {LeadService} from 'services/lead/lead-service';
import {QueryResolvers, Lead} from 'generated/graphql/generated-resolver';
import { Title } from 'generated/proto/share_pb';
import { MyContext } from "context";

interface Resolvers {
  Query: QueryResolvers;
}

export const leadResolver: Resolvers = {
  Query: {
    ping: async(root, args, context: MyContext) => {
      console.log(context.authToken);
      let result = await new LeadService(context).ping();
      return result;
    },

    leads: async (root, args, context) => {
      let leads = await new LeadService(context).getLeads();

      return leads.map(c => <Lead> {
        firstName: c.firstname,
        leadOwner: c.leadowner,
        company: c.company,
        lastName: c.lastname,
        title: _.findKey(Title, (o) => o == c.title),
        address: c.address
      });
    },
  },
};
