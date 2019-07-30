import * as grpc from 'grpc';
import { Debugger, debug } from 'debug';
import { Empty } from 'google-protobuf/google/protobuf/empty_pb';

import { LeadClient } from 'generated/proto/lead_grpc_pb';
import { LEAD_SERVICE_URL } from 'config';
import { LeadInformation } from 'generated/proto/lead_pb';
import { MyContext } from 'context';

export class LeadService {
  private client: LeadClient;
  private log: Debugger;

  public constructor(context: MyContext) {
    this.log = debug('crm-grpc');
    
    let interceptorAuth = (options: any, nextCall: any) => {
      return new grpc.InterceptingCall(nextCall(options), {
        start: (metadata, listener, next) => {
          metadata.add("Authorization", context.authToken);
          next(metadata, listener);
        }
      });
    }
    this.client = new LeadClient(
      LEAD_SERVICE_URL,
      grpc.credentials.createInsecure(),
      {
        interceptors: [interceptorAuth]
      }
    );
  }

  public ping() {
    return new Promise<string>((resolve, reject) => {
      this.log(`[lead-service][ping]`);

      this.client.ping(new Empty(), (err, response) => {
        if (err != null) {
          this.log(
            `[lead-service][ping] err:\nerr.message: ${err.message}\nerr.stack:\n${err.stack}`
          );
          reject(err);
          return;
        }
        resolve(response.toObject().message);
      });
    });
  }

  public getLeads() {
    return new Promise<LeadInformation.AsObject[]>((resolve, reject) => {
      this.log(`[lead-service][get leads]`);

      this.client.getLeads(new Empty(), (err, response) => {
        if (err != null) {
          this.log(
            `[lead-service][get leads] err:\nerr.message: ${err.message}\nerr.stack:\n${err.stack}`
          );
          reject(err);
          return;
        }
        resolve(response.toObject().leadsList);
      });
    });
  }
}
