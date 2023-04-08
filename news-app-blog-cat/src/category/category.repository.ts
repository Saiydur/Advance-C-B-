import { EntityRepository, Repository } from "typeorm";
import { Category } from "./category.entity";

@EntityRepository(Category)
export class CategoryRepository extends Repository<Category> {
    findOneById(id: string): Promise<Category> {
        return this.createQueryBuilder('category')
            .where('category.id = :id', { id })
            .getOne();
    }
}