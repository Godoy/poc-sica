import React from 'react';
import { Admin, Resource } from 'react-admin';
import jsonServerProvider from 'ra-data-json-server';
import { AssetList, AssetEdit, AssetCreate } from './resources/asset';

const dataProvider = jsonServerProvider('http://localhost:65487/api');

const App = () => (
  <Admin dataProvider={dataProvider}>
    <Resource name="assets" list={AssetList} edit={AssetEdit} create={AssetCreate} />
  </Admin>
);

export default App;