import React from 'react';
import { List, Datagrid, TextField, DateField, EditButton, SimpleForm, 
    Edit, DateInput, TextInput, DateTimeInput, Create  } from 'react-admin';

const AssetTitle = ({ record }) => {
    return <span>Asset {record ? `"${record.model}"` : ''}</span>;
};

export const AssetList = props => (
    <List {...props}>
        <Datagrid rowClick="edit">
            <TextField source="id" />
            <TextField source="model" />
            <DateField source="purchasedAt" />
            <DateField source="maintenanceOn" showTime  />
            <EditButton />
        </Datagrid>
    </List>
);

export const AssetEdit = props => (
    <Edit title={<AssetTitle />} {...props}>
        <SimpleForm>
            <TextInput disabled source="id" />
            <TextInput source="model" />
            <TextInput multiline  source="description" />
            <DateInput source="purchasedAt" />
            <DateTimeInput source="maintenanceOn" />
        </SimpleForm>
    </Edit>
);

export const AssetCreate = props => (
    <Create {...props}>
        <SimpleForm>
            <TextInput source="model" />
            <TextInput multiline  source="description" />
            <DateInput source="purchasedAt" />
            <DateTimeInput source="maintenanceOn" />
        </SimpleForm>
    </Create>
);

