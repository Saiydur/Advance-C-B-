import { Body, Controller, Get, Post } from '@nestjs/common';
import { CategoryService } from './category.service';
import { ClientOptions, ClientProxy, ClientProxyFactory, EventPattern, MessagePattern, Transport } from '@nestjs/microservices';
import { Category } from './category.entity';

@Controller("categories")
export class CategoryController {
  private readonly client: ClientProxy;
  constructor(private readonly categoryService: CategoryService,
    ) {
    const options: ClientOptions = {
      transport: Transport.RMQ,
      options: {
        urls: ['amqp://localhost:5672'],
        queue: 'category_queue',
      },
    };
    this.client = ClientProxyFactory.create(options);
  }

  @Post('categories')
  async createCategory(@Body() category: any) {
    console.log(category);
    const createdCategory = await this.categoryService.create(category);
    const pattern = { cmd: 'categoryCreated' };
    this.client.emit(pattern, createdCategory);
    return createdCategory;
  }

  @EventPattern({cmd: 'getCategories'})
  async getCategories() {
    const categories = await this.categoryService.findAll();
    return categories;
  }

  @Get()
  async findAll() {
    const pattern = { cmd: 'getCategories' };
    const categories = await this.client.send<Category[]>(pattern, {}).toPromise();
    return categories;
  }

  @EventPattern('categoryCreated')
  async handleCategoryCreated(category: Category) {
    return "Created";
  }
}
