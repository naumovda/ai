from copy import deepcopy

colors = {
        0: None,
        1:"green", 
        2:"red", 
        3:"blue", 
        4:"yellow" 
        # 5:"cyan", 
        # 6:"black"
    }

NONE_COLOR = 0
COLORS_SET = set(colors.keys()) - {NONE_COLOR}

class GeoMap:
    def __init__(self, geomap):        
        self.map = geomap
        self.regions = [region for region in self.map.keys()]
        self.regions.sort(key=lambda region: len(self.get_neighbors(region)),
                          reverse=False)
        
    def get_neighbors(self, region):
        return self.map[region][1]

    def get_aviable_color(self, region, painting):
        neighbors = self.get_neighbors(region)
        neigcolors = set()
        for neighbor in neighbors:
            if neighbor in painting:
                neigcolors.add(painting[neighbor])

        return COLORS_SET - neigcolors

class MapState:
    def __init__(self, geomap, painting=None, depth=0, parent=None):
        self.map = geomap                
        if painting is None:
            # self.painting = {region: NONE_COLOR for region in self.map.regions}
            self.painting = dict()
        else:
            self.painting = painting
        self.depth = depth
        self.parent = parent

    def __hash__(self):
        hash_ = 0
        for region in self.map.regions:
            hash_ = hash_ * 10
            if region in self.painting:
                 hash_ += self.painting[region]
        return hash_

    def __eq__(self, other):
        return hash(self) == hash(other)

    def get_moves(self):
        result = []
        for region in self.map.regions:
            if region not in self.painting:
                for color in self.map.get_aviable_color(region, self.painting):                   
                    move = MapState(self.map, deepcopy(self.painting), 
                                    self.depth + 1, self)
                    move.painting[region] = color                
                    result.append(move)
                return result

    def valid_color(self, region):
        color = self.painting[region]
        for neighbor in self.map.get_neighbors(region):
            neigcolor = self.painting[neighbor]
            if (neigcolor is not None) and (color == neigcolor):
                return False

        return True

    def is_goal(self):
        # valid = [self.valid_color(region) for region in self.map]
        # colors = [color for color in self.painting.values()]
        # return all(valid) and not None in colors
        return len(self.painting) == len(self.map.regions)

    def check_valid(self):
        valid = [self.valid_color(region) for region in self.map.regions]
        return all(valid), valid

if __name__ == "__main__":
    import dfs
    from graphs import USA_CUT        
    
    geo = GeoMap(USA_CUT)

    initial = MapState(geo)

    print(initial.painting)

    # moves = initial.get_moves()
    # for move in moves:
    #     print(move.painting)

    _, goal, open_states, close_states = dfs.dfs(initial, None)

    print(goal.painting)
    print("states = ", open_states, close_states)

    valid, check = goal.check_valid()
    print(valid, check)
