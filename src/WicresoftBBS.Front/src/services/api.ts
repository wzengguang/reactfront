import { get, post, put } from "./fetch"

export async function gePostDetail(id: string) {
    return await get('/api/posts/' + id);
}
