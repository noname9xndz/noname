//https://github.com/solrevdev/blazor-on-vercel
install vercel cli

create publish/wwwroot/vercel.json

{
    "version": 2,
    "routes": [{"handle": "filesystem"}, {"src": "/.*", "dest": "/index.html"}]
}

<ItemGroup>
    <None Include="publish/wwwroot/vercel.json"  CopyToPublishDirectory="PreserveNewest" />
  </ItemGroup>

run deploy.sh