import cookie from 'react-cookies';
interface HttpResponse<T> extends Response {
  result?: T;
}

function requestHeader() {
  const token = cookie.load("Token");
  const value = token ? token["Token"] : "";
  return { "Authorization": "bearer " + token };
}
async function http<T>(request: RequestInfo): Promise<HttpResponse<T>> {
  const response: HttpResponse<T> = await fetch(request);
  try {
    response.result = await response.json();
  } catch (ex) { }

  if (!response.ok) {
    throw new Error(response.statusText);
  }
  return response;
}

export async function get<T>(
  path: string,
  args: RequestInit = { method: "get", headers: requestHeader() }
): Promise<HttpResponse<T>> {
  return await http<T>(new Request(path, args));
};

export async function post<T>(
  path: string,
  body: any,
  args: RequestInit = { method: "post", body: JSON.stringify(body), headers: requestHeader() }
): Promise<HttpResponse<T>> {
  return await http<T>(new Request(path, args));
};

export async function put<T>(
  path: string,
  body: any,
  args: RequestInit = { method: "put", body: JSON.stringify(body), headers: requestHeader() }
): Promise<HttpResponse<T>> {
  return await http<T>(new Request(path, args));
};