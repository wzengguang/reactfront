import { DashboardOutlined } from '@ant-design/icons';
import Loader from './loader';

// TODO: 多维无限嵌套， 按照目录分离单个文件
// type RoutesItem<K extends string> = {
//   [k in K]: RoutesItem<K>[];
// };

// type Flatten<K extends string, T extends RoutesItem<K>> = T[];

// function flatten<K extends string, T extends RoutesItem<K>>(data: T[], k: K & keyof T): Flatten<K, T>;

// 菜单Item
export type RouteItem = {
  name: string;
  path?: string;
  key?: string;
  auth?: boolean;
  hideInMenu?: boolean; // 是否显示在菜单， 默认flase
  icon?: any;
  routes?: RouteItem[];
  exact?: boolean;
  component?: React.ComponentType;
  redirect?: string;
};

// 不需要权限
const noAuthMenus: RouteItem[] = [
  {
    name: '登录页',
    exact: true,
    path: '/login',
    component: Loader(() => import('@/pages/login')),
  },
  {
    name: '注册页',
    exact: true,
    path: '/register',
    component: Loader(() => import('@/pages/login')),
  },
];

export default noAuthMenus;
