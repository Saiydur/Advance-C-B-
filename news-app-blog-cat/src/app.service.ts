import { Injectable } from '@nestjs/common';
import { ClientProxy, ClientProxyFactory, Transport } from '@nestjs/microservices';
import { Category } from './category/category.entity';
@Injectable()
export class AppService {
  private client: ClientProxy;
  constructor() {
    this.client = ClientProxyFactory.create({
      transport: Transport.RMQ,
      options: {
        urls: ['amqp://localhost:5672'],
        queue: 'category_queue',
      },
    });
  }

  async getCategories(): Promise<Category[]> {
    return this.client.send<Category[], any>('getCategories', {}).toPromise();
  }
}
